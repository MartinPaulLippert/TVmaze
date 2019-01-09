# TVmaze
Connects to the TVmaze API and gets shows

## Details

* The application uses LiteDB (http://www.litedb.org/) as a simple lightwieght data store
* The application has a hosted service called TVmazeTimedService
  * This service has a timer that run every hour
  * It gets the list of show ids and the last timestamp of the last update
  * It checks to see if the show exists and has been update, if it hasn't it goes and gets the show details with the cast details
  * The show details are then added or updated in the database
  * If the service receives a repsonse with the status code 429 from the TVmaze api, the service will wait for 10 seconds before querying again.
 * The application has a api call which is accessible at \api\shows
  * This provides a list of all tv shows, the shows contain a list of cast members
  
## Possible issues

* It is possible that a tv show might not be loaded into the database because of an issue deserializing the json to the POCO. Any errors parsing the show details
are logged and can be corrected by fixing the POCOs.

## Possible Improvements

* More logging message
* Unit testing
 
  
