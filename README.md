# MobilePhonesOntology
# ASP.NET Project presenting ontology of mobile phones

It is not possible to extract a full list of phones from the fonoapi. I noticed that the names of the phones are very similar to those on the phonegg, so I parsed the page with the phone brands and subpages of these brands, bringing out the full list of phones and built them a graph of "brandAndModels". 
I created a function that retrieves the access token to fonoapi. Then for each phone I tried to download it from the fonoapi, and the downloaded phones were deserialized from Json to my own model. I created triples of these data and added it to the graph. I saved these graphs into a files with the structure of notation3 (a format similar to XML, but easier to read by a human), which will be loaded at the start of the application. 
I also created tasks updating my data structures at night - at 4:00 am I update the brandsAndModels graph, and at 4:30 the Phones graph. 
The project uses dotnetrdf extension, which allows me to easily manage data. 
The whole was written in asp.net mvc using the principles of good programming. In a few places, I had to overload the standard functions to increase the readability of my code. In order to ensure high reliability, the functionality has been thoroughly tested using unit tests.
