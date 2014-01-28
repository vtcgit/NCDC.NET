## NCDC.NET
NCDC.NET is .NET class library that provides access to the Nationcal Climatic Data Center hosted by the National Oceanic and Atmospheric Administration (NOAA). <http://www.ncdc.noaa.gov/cdo-web/webservices/ncdcwebservices>

## Design
The design of NCDC.NET is similar to Twitterizer (located [here](https://github.com/Twitterizer/Twitterizer)) which allows for easy extension to the class library.

## Installation
1. `git clone` to your machine
2. Add as a project reference using the `*.csproj` file	
	- Alternatively, Build the project add the `*.dll`

## Usage
Here are a few examples utilizing the library. There are many others

### Fetching Available Datasets

    NCDCDataset.GetDataSets()
	NCDCDataset.GetDatasetInformation(string datasetId)

### Fetching Station Locations for a given data set

	NCDCLocation.GetLocations(string datasetId);
	NCDCLocation.GetLocations(string datasetId, string locationTypeName);
	NCDCLocation.GetLocationInformation(string datasetId, string locationTypeName, string locationId);
	NCDCLocation.GetLocationInformation(string datasetId, string locationId);

### Fetching Data

        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeLocationStationAndDataType(string datasetId,
            string locationTypeId, string locationId, string stationId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForDataset(string datasetId)
        public static NCDCResponse<NCDCDataCollection> GetDataForDataType(string datasetId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationAndDataType(string datasetId, string locationId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationStationAndDataType(string datasetId, 
            string locationId, string stationId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationAndStation(string datasetId, string locationId, string stationId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeAndDataType(string datasetId,
            string locationTypeId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeLocationAndDataType(string datasetId,
            string locationTypeId, string locationId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeLocationAndStation(string datasetId,
            string locationTypeId, string locationId, string stationId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationTypeAndLocation(string datasetId,
            string locationTypeId, string locationId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocationType(string datasetId, string locationTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForLocation(string datasetId,
            string locationTypeId, string locationId, string stationId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForStationAndDataType(string datasetId, string stationId, string dataTypeId)
        public static NCDCResponse<NCDCDataCollection> GetDataForStation(string datasetId, string stationId)