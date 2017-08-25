# _Band Tracker_

#### _A web app that tracks bands and the venues where they've played concerts, Aug 18, 2017_

#### By _**Charlie Kelson**_

## Description

_This web app will allow a user to create bands that have played at a venue. The user will be able to view what bands have played at what venues and what venues have hosted which bands._


### User Story

| User Behavior | Input | Output |
|----|----|----|  
| As a user, I need to be able to see a list of all the venues. | Venue | Venue list |
| As a user, I need to add new venues. | Add venue | List of venues |
| As a user, I need to be able to select a single venue, view its details, and see a list of all bands that belong to that venue. | Click venue name | Venue details with list of bands|
| As a user, I need to be able to add new bands to a specific venue. | Add band | Band list on venue page|
| As a user, I need to be able to update a venue's name. | Update venue name | New venue name|
| As a user, I need to be able to delete a venue if it no longer exists. | User clicks delete button| Venue is deleted|
| As a user, I need to be able to select a band, view its details, and see a list of all venues that belong to that band. | Click band name | Band details with list of venues|


### Technical Specs

| App Behavior | Expected | Actual |
|----|----|----|  
|  Get all venues at first position in database (READ) | 0 | Database List<Venue> count start at 0 |
|  Save venue to database (CREATE)|  Local List<Venue> = {Gorge}  | Database List<Venue> = {Gorge}   |
|  Find venue from database by id (READ)|  Gorge  |  Gorge  |
|  Update venue name (UPDATE)| Crocodile | Crocodile |
|  Delete venue (DELETE)|A list of only one venue rather than two | A database query that only returns one venue after delete method has been called |
| Get all bands at first position in database (READ)| 0 | Database List<Bands> count start at 0|
|  Save band to database (CREATE)| List with one band: Nirvana | List with one band: Nirvana |
|  Find band from database by id (READ)| Ween  |  Ween  |
|  Save band to database (JOIN STATEMENT) by venue_id| List with one band: Nirvana | List with one band: Nirvana |
|  Get all bands of a specific venue in join table (JOIN STATEMENT) after user clicks specific venue on homepage|  Local band list  |  Database band list matches local band list  |
|  Get all venues of a specific band in join table (JOIN STATEMENT) after user clicks on a specific band in the all band lists (button on the homepage)|  Local venue list  |  Database venue list matches local venue list  |

![](/schema.png)


## Setup/Installation Requirements

* _Clone repo and set up .NET dependencies to view locally_
* _Configure MySQL database with MAMP and recreate database with instructions below_
---

#### MySQL commands to create database
- `CREATE DATABASE band_tracker;`
- `USE band_tracker;`
- `CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255));`
- `CREATE TABLE bands (id serial PRIMARY KEY, name VARCHAR(255));`
- `CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id INT, venue_id INT);`

---

## Known Bugs

_No known Bugs_



## Technologies Used

* _ASP.NET MVC_
* MySQL

### License

MIT License

Copyright (c) 2017 **_Charlie Kelson_**
