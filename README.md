A small assignment simulating a air traffic control system.

The app reads area (Zone) inputs from a .map file (prompted at startup, both valid and invalid sample data provided in SampleData/). These are provided in lines and contain information about an area's shape, coverage in XY coordinates and its "danger level" (safe/warn/shoot).

The app then starts listening for input containing flight callsigns and coordinates through Console.ReadLine(), parses it, and if valid, compares all flights with the covered areas. If the flight is in an area marked as 'warn' or 'shoot', this information is appropriately printed to console.

Written in .NET6
