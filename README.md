Simple flight search app

Built with C# API and Vue TS

Split into 4 parts
- External 
- Data
- Server side
- Client

Connects to [Amadeus Flight Offer API](https://developers.amadeus.com/self-service/category/flights/api-doc/flight-offers-search)

Amadeus credentials are required to run the solution. All settings are empty because appsettings are not uploaded with values.

Only 2 tests to show that caching works.

By default the DB is not used, but there is an option in the settings to use it, as this was a requirement of the task.
By my logic, fetching data about any flight ticket should have a max life under 20 minutes as they can be bought in that time and most sales systems reserve them for a period of 15 minutes.
When using a database, each response is also stored in the DB and at the start of the project all still "valid" responses are preloaded into the cache.

Responses to requests are always cached for the duration set in the preferences. 

Frontend is rudimentary and only shows required data in a simple table.

It would be better if the API endpoint would also respond with a status code or some sort of result, but I need to know FE a bit better.

Models for Amadeus API and FE calls are auto-generated with NSwag.
