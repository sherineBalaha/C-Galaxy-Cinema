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
    public class MovieImporter : BaseImporter
    {
        //instance created
        ImportResult results  ;

        //declarations
        string fileData  = null;
        string firstLine = null;
        string[] columns = null;
        int lineNum = 0 ;
        int lineCount = 0;

        //stringBuilder declarations
        string titleString = null;
        string movieIdString = null;

        //constructor
        public MovieImporter (string fileName) :base (fileName)
        {
            
        }

        /// <summary>
        /// Import movie file. Filename has been provided in the constructor.
        /// </summary>
        public override void Import(object o)
        {
            // Initialise progress to zero for progress bar.

            try
            {
                // Read file
                using (StreamReader reader = File.OpenText(fileName))
                {
                    // Read file  using ReadToEnd
                    fileData = reader.ReadToEnd();


                }

                // To deal with Windows, Mac and Linux line endings the same.
                // Skip blank lines
                string[] lines = fileData.Replace("\r\n", "\n").Replace("\r", "\n").Split('\n');


                foreach (string line in lines)
                {


                    //check number ofcolums
                    if (columns.Length != 2)
                    {
                        results.FailedRows++;
                        results.ErrorMessages.Add(string.Format("Line {0}: Wrong number of columns.", lineNum));
                        continue;
                    }

                    // Build a string MovieId
                    StringBuilder stringBuilderMovieId = new StringBuilder();
                    stringBuilderMovieId.Append(columns[0].Trim());
                    movieIdString = stringBuilderMovieId.ToString().ToLower();

                    //Console.WriteLine(movieId);

                    // Build a string Title
                    StringBuilder stringBuilderTitle = new StringBuilder();
                    stringBuilderTitle.Append(columns[1].Trim());
                    titleString = stringBuilderTitle.ToString().ToLower();

                    //Console.WriteLine(title);

                    // Check if first line is column names.

                    if (line.StartsWith("MovieID"))
                    {
                        firstLine = line;
                        // Break up line by commas, each item in array will be one column.
                        columns = firstLine.Split(',');

                        if (movieIdString == "movieid" && titleString == "title")
                        {
                            lines[0] = "";
                        }


                    }
                }

                // Line count and line numbers to allow progress tracking.
                //number of rows 
                lineCount = lines.Length;
                //row number
                lineNum = 1;

                // Get all movies.
                List<Movie> movies = new List<Movie>();
                movies = GalaxyCinemas.DataLayer.GetAllMovies();

                try
                {
                    //progress bar loop
                    foreach (string line in lines)
                    {
                        // Check whether we need to stop after importing each line.
                        if (Stop)
                        {
                            return;
                        }


                        // Just to make it slow enough to test stopping functionality.
                        Thread.Sleep(500);

                        // Update progress of import.


                        // Check the format of data, and update ImportResult accordingly.
                        int movieID = 0;
                        string title = null;

                        if (!int.TryParse(columns[0], out movieID) && (title.Equals("")))
                        {
                            results.FailedRows++;
                            results.ErrorMessages.Add(string.Format("Line {0}: MovieID is not a number.", lineNum));
                            results.ErrorMessages.Add(string.Format("Line {0}: Title is empty.", lineNum));
                            continue;
                        }


                        // Insert/update DB if okay.
                        Movie movieToUpdate = movies.Where(m => m.MovieID == movieID).FirstOrDefault();
                        if (movieToUpdate == null)
                        {
                            Movie movieToAdd = new Movie() { MovieID = movieID, Title = title };
                            DataLayer.AddMovie(movieToAdd);
                        }
                        else
                        {
                            movieToUpdate.Title = title;
                            DataLayer.UpdateMovie(movieToUpdate);
                        }

                        results.ImportedRows++;

                    }
                }
                finally
                {
                    lineNum++;

                }



            }//outer try braces


            //outer catch blocks
            catch (IOException ioe)
            {
                results.ErrorMessages.Add(string.Format(ioe.Message.ToString(), lineNum));

            }
            catch (Exception e)
            {
                results.ErrorMessages.Add(string.Format(e.Message.ToString(), lineNum));
            }

            finally
            {
                //raise the completed event
            }
            
        }

    }
}
