# Showtime, a Band Tracking Website

#### An epicodus project using C# and SQL

#### **By David Wilson**

## Description

This web application will allow the user to add bands and venues. They will be able to delete and/or edit a bands and venues, and add bands to venues (and vice-versa).

<!-- ![alt text](https://github.com/GrapeSalad/Hair-Salon/blob/master/Content/img/barbershop.gif "Barber Of Seville") -->

#### RESTful Routing
| Behavior | Input | Output |
|---|---|---|
| Home Page | "localhost:5004/" | home.cshtml |
| View Venues | "localhost:5004/venues" | venues.cshtml |
| Add Venues | "localhost:5004/venues/add" | venues_add_form.cshtml |
| View Bands | "localhost:5004/bands" | bands.cshtml |
| Add Bands | "localhost:5004/bands/add" | bands_add_form.cshtml |
| View specific venue | "localhost:5004/venues/1" | venue.cshtml |
| View specific band | "localhost:5004/bands/1" | band.cshtml |
| Edit Venue | "localhost:5004/venues/1/edit" | venue.cshtml |
| Delete Band | "localhost:5004/bands/1/delete" | band_delete.cshtml |

### SPECS
1.  List all Venues
2.  List all Bands
3.  See Venue details (name, city, list of Bands)
4.  Add Venues/Bands
5.  Add Bands to Venue/Venues to Band
6.  Update Venue/Band information
7.  Delete Venue/Band information

| Behavior | Input | Output |
|---|---|---|
| User enters a venue name, city | "Lovecraft, Portland" | "Lovecraft, Portland" |
| User enters band name | "The Number 12 Looks Like You" | "The Number 12 Looks Like You" |
| User realizes error, needs to edit Venue name | "Lvcrft" | "Lvcrft" --> "Lovecraft" |
| User notices band is no longer together | Delete "Miguel" | success |

## Setup/Installation Requirements

1.  Go to the Github repository page at https://github.com/GrapeSalad/Showtime
2.  Click the "download or clone" button and copy the link.
3.  In your Microsoft Windows installation location open PowerShell.
4.  In PowerShell navigate to the directory in which you want to store the app files.
5.  Clone using "git clone " and the copied link.
6.  To run the project locally you will need Mono Compiler (http://www.mono-project.com/download/#download-win), Visual Studio 2015 (https://go.microsoft.com/fwlink/?LinkId=532606), and ASP.Net 5 (https://go.microsoft.com/fwlink/?LinkId=627627).
You will also need SSMS (https://go.microsoft.com/fwlink/?linkid=849819).
7.  Setting up the database will occur after installation and downloading. In PowerShell run the command: sqlcmd -S "(localdb)\mssqllocaldb". Now type SSMS into your windows search bar and open SQL Server Management Studio.
8.  To connect to the appropriate database type (localdb)\mssqllocaldb into the "server name" field in the popup window.
9.  I have included a few SQL scripts to help you start up your server, they can be found in the SQL_Queries folder. Double click on them and open them with SSMS, and then execute the one titled band_tracker_setup.
10. One more query to execute is band_tracker_test_setup. This will set up a testing environment for the Xunit Tests.
11.  Once those are all installed and the tables are created, restart PowerShell and enter "dnvm upgrade" into the prompt.
12.  Now you can navigate to the directory in which you downloaded the Band_Tracker App, and then into the Band_Tracker home file.
13.  To start the web server necessary for proper app functionality you will need to type in "dnu restore" then "dnx kestrel".
14.  Open a web browser, and type "localhost:5004".
To view the code you can open the files using your editor of choice.
15. If you have the editor path set in your system properties you will be able to open them through PowerShell.

## Known Bugs



## Support and Contact Details

If you have any issues, questions, ideas, concerns, or contribution ideas please contact any of the contributors via Github.

## Technologies used

* C-Sharp
* CSS
* Javascript
* HTML
* Boostrap
* JSON
* DVNM
* PowerShell
* Google Chrome
* Razor
* Nancy
* xUnit
* SQL
* SSMS

### License

This software is licensed under the MIT license.

Copyright (c) 2017 **David Wilson**
