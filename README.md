# Hotel Booking Specflow .Net Example

[![CircleCI](https://circleci.com/gh/hindsightsoftware/hotel-booking-specflow-dotnet-example.svg?style=svg)](https://circleci.com/gh/hindsightsoftware/hotel-booking-specflow-dotnet-example)

## Usage

First, run the [Hotel Booking app](https://github.com/hindsightsoftware/hotel-booking). The easiest way is to do it via Docker as shown below. This will start the app that can be accessed at <http://localhost:8080>

```bash
docker run --rm -p 8080:8080 --name=hotel-booking -itd hindsightsoftware/hotel-booking:latest
```

Next, open the project on the Visual Studio as a solution(sln) project and run the tests from IDE by clicking `Test -> Run all tests`. The report will be generated as a JSON file in `TestResults/Report.json`.

Alternatively run the tests by running dotnet test from the project folder.