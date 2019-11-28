using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;


//added statement to reference Business_Objects Library
using Common.Business_Objects;


namespace GalaxyCinemas
{
    public class SessionImporter : BaseImporter
    {
        //instance created
        ImportResult results;

        //declarations
        string fileData = null;
        string firstLine = null;
        string[] columns = null;
        int lineNum = 0;
        int lineCount = 0;

        //stringBuilder declarations
        string sessionIdString = null;
        string movieIdString = null;
        string sessionDateString = null;
        string cinemaNumberString = null;
         


        public SessionImporter(string filename) : base(filename)
        {
        }

        /// <summary>
        /// Import session file. Filename has been provided in the constructor.
        /// </summary>
        public override void Import(object o)
        {
            // Initialise progress to zero for progress bar.
            Progress = 0f;
            ImportResult results = new ImportResult();
            try
            {
                // Read file
                string fileData = null;
                using (StreamReader reader = File.OpenText(fileName))
                {
                    fileData = reader.ReadToEnd();
                }

                // To deal with Windows, Mac and Linux line endings the same.
                // Skip blank lines
                string[] lines = fileData.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');

               
                foreach (string line in lines)
                {

                    //increase totalRows
                    results.TotalRows++;

                    if (columns.Length != 4)
                    {
                        results.FailedRows++;
                        results.ErrorMessages.Add(string.Format("Line {0}: Wrong number of columns.", lineNum));
                        continue;
                    }

                    // Build a string SessionId
                    StringBuilder stringBuliderSessionID = new StringBuilder();
                    stringBuliderSessionID.Append(columns[0].Trim());
                    sessionIdString = stringBuliderSessionID.ToString().ToLower();

                    // Build a string MovieId
                    StringBuilder stringBuilderMovieId = new StringBuilder();
                    stringBuilderMovieId.Append(columns[0].Trim());
                    movieIdString = stringBuilderMovieId.ToString().ToLower();


                    // Build a string sessionDate
                    StringBuilder stringBuildersessionDate = new StringBuilder();
                    stringBuilderMovieId.Append(columns[0].Trim());
                    sessionDateString = stringBuildersessionDate.ToString().ToLower();

                    // Build a string cinemaNumber
                    StringBuilder stringBuildercinemaNumber = new StringBuilder();
                    stringBuilderMovieId.Append(columns[0].Trim());
                    cinemaNumberString = stringBuildercinemaNumber.ToString().ToLower();
                   
                    
                    // Check if first line is column names.

                    if (line.StartsWith("SessionID"))
                    {
                        firstLine = line;

                        // Break up line by commas, each item in array will be one column.
                        columns = firstLine.Split(',');

                        if (sessionIdString == "sessionid" && movieIdString == "movieid" && sessionDateString == "sessiondate" && cinemaNumberString == " cinemanumber")
                        {
                            lines[0] = "";
                        }


                    }
                }


                // Line count and line numbers to allow progress tracking.
                lineCount = lines.Length;
                lineNum = 1;

                
                // Get all movies.
                List<Movie> movies = new List<Movie>();
                movies = GalaxyCinemas.DataLayer.GetAllMovies();
                

                // Get all sessions. These will be used to check that sessionID are valid.
                List<Session> sessions = new List<Session>();
                
                foreach (string line in lines)
                {
                    // Check whether we need to stop after importing each line.
                    if (Stop)
                    {
                        return;
                    }

                    try
                    {
                        // Just to make it slow enough to testing stopping functionality.
                        Thread.Sleep(500);

                        // Update progress of import.
                       
                        

                        // Build a string SessionId
                        StringBuilder stringBuilderSessionId = new StringBuilder();
                        stringBuilderSessionId.Append(columns[0].Trim());
                        sessionIdString = stringBuilderSessionId.ToString().ToLower();

                        // Build a string MovieId
                        StringBuilder stringBuilderMovieId = new StringBuilder();
                        stringBuilderMovieId.Append(columns[1].Trim());
                        movieIdString = stringBuilderMovieId.ToString().ToLower();

                        //Console.WriteLine(movieId);

                        // Build a string SessionDate
                        StringBuilder stringBuilderSessionDate= new StringBuilder();
                        stringBuilderSessionDate.Append(columns[2].Trim());
                        sessionDateString = stringBuilderSessionDate.ToString().ToLower();

                        //Console.WriteLine(title);

                        // Build a string cinemaNumber
                        StringBuilder stringBuilderCinemaNumber = new StringBuilder();
                        stringBuilderCinemaNumber.Append(columns[3].Trim());
                        cinemaNumberString = stringBuilderCinemaNumber.ToString().ToLower();

                        // Check if first line is column names.

                        if (line.StartsWith("SessionID"))
                        {
                            firstLine = line;
                            // Break up line by commas, each item in array will be one column.
                            columns = firstLine.Split(',');

                            if ((sessionIdString == "sessionid" && movieIdString == "movieid") && (sessionDateString == "sessiondate") && (cinemaNumberString == "cinemanumber"))
                            {
                                lines[0] = "";
                            }


                        }

                        ///////
                        // Check the format of data, and update ImportResult accordingly.
                        // Check session ID.
                        int sessionID = 0;
                        if (!int.TryParse(columns[0].Trim(), out sessionID))
                        {
                            results.FailedRows++;
                            results.ErrorMessages.Add(string.Format("Line {0}: sessionID is not a number.", lineNum));
                            continue;
                        }

                        // Check movie ID.
                        int movieID = 0;
                        if (!int.TryParse(columns[1].Trim(), out movieID))
                        {
                            results.FailedRows++;
                            results.ErrorMessages.Add(string.Format("Line {1}: MovieID is not a number.", lineNum));
                            continue;
                        }
                       
                        // Check session date/time.
                        DateTime sessionDate = DateTime.MinValue;
                        if (!DateTime.TryParse(columns[2].Trim(), out sessionDate))
                        {
                            results.FailedRows++;
                            results.ErrorMessages.Add(string.Format("Line{2}: Session date is not a date/time", lineNum));
                            continue;
                        }

                        // Check cinema number.
                        byte cinemaNumber = 0;
                        if ( (!byte.TryParse(columns[3].Trim(), out cinemaNumber)) && (cinemaNumber >= 1) )
                        {
                            results.FailedRows++;
                            results.ErrorMessages.Add(string.Format("Line {3}: cinemaNumber must be positive", lineNum));
                            continue;
                        }
                       
                        // Insert/update DB if okay.
                        Session sessionToUpdate = DataLayer.GetSessionByID(sessionID);
                        if (sessionToUpdate == null)
                        {
                            Session sessionToAdd = new Session() {SessionID = sessionID,  MovieID = movieID, SessionDate = sessionDate, CinemaNumber =  cinemaNumber};
                            DataLayer.AddSession(sessionToAdd);
                        }
                        else
                        {
                            sessionToUpdate.MovieID = movieID;
                            sessionToUpdate.SessionDate = sessionDate;
                            sessionToUpdate.CinemaNumber =  cinemaNumber;

                            DataLayer.UpdateSession(sessionToUpdate);
                        }

                        results.ImportedRows++;
  
                    }
                    catch (System.Data.Common.DbException dbEx)
                    {
                        results.FailedRows++;
                        results.ErrorMessages.Add(string.Format("Line {0}: Database error occurred updating data.", lineNum));
                    }
                    finally
                    {
                        lineNum++;
                    }
                }

            }//outer try braces

            //outer catch blocks
            catch (System.IO.IOException)
            {
                results.ErrorMessages.Add("Error occurred opening file. Please check that the file exists and that you have permissions to open it.");
            }
            catch (Exception)
            {
                results.ErrorMessages.Add("An unknown error occurred during importing.");
            }
            finally
            {
                // Do callback to end import.
                RaiseCompleted(results);
            }

        
        }

    }
}
