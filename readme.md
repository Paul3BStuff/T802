# T802

## Formula Runner

Formula Runner is a program written in support of a research project for a masters degree with the Open University.

The program reads a file of sample code and creates readabillty scores using six common readability indices.

Parts of the code use a library written by [ttsu/TextStatistics.Net](https://github.com/ttsu/TextStatistics.Net).

Requires Visual Studio 2015 or higher express version should be adequate.

_Usage:_
After compilation the program is executed with two parameters:
- The first is the path to the samples file.
- The second is the folder to place the results folder.

The output parameter specifies a path to a folder which will, after processing contains a folder
called Output-_ddmmyyyy-hhmmss_ this folder contains the four output file.

The four output files are:
* Summary.csv: results of process at the sample level.
* Sentences.csv: results of processing at sentence level.
* Words.csv: properties of words.
* Cumulative.csv: Contains all properties from all of the files above.

The csv file should be readable in any program that can read csv files although it has only been used with Excel.
The seperator between each column is the tilde '~'.
 