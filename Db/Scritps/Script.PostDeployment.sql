/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

Set Identity_Insert [villaSchema].[Tbl_Villas] On
    Insert 
    Into [villaSchema].[Tbl_Villas] 
    (   [Id]
        , [Name]        
        , [Details]    
        , [Rate]          
        , [Sqft]         
        , [Occupancy]     
        , [ImageUrl]      
        , [Amenity]         
    )
    Values 
    (
    1
    , 'Royal Villa'
    , 'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.'
    , 2.5
    , 550
    , 4
    , 'https://domain.com/image/villa1.jpg'
    , 'Near to main road'
    ),
    (
    2
    , 'Premium Pool Villa'
    , 'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.'
    , 3.5
    , 550
    , 4
    , 'https://domain.com/image/villa2.jpg'
    , 'Near to main road'
   ),
    (
    3
    , 'Luxury Pool Villa'
    , 'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.'
    , 4.2
    , 750
    , 4
    , 'https://domain.com/image/villa3.jpg'
    , 'Lorem ipsum'
    ),
    (
    4
    , 'Diamond Villa'
    , 'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.'
    , 5.5
    , 900
    , 4
    , 'https://dotnetmastery.com/bluevillaimages/villa4.jpg'
    , 'Lorem ipsum'
    ),
    (
    5
    , 'Diamond Pool Villa'
    , 'Fusce 11 tincidunt maximus leo, sed scelerisque massa auctor sit amet. Donec ex mauris, hendrerit quis nibh ac, efficitur fringilla enim.'
    , 6.0
    , 1100
    , 4
    , 'https://domain.com/image/villa5.jpg'
    , 'Lorem ipsum'
    )

