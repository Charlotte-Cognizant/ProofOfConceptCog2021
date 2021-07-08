Instructions for insrtalling OSMnx and documentation: https://osmnx.readthedocs.io/en/stable/

Enter any valid address string as first cmd argument. 
Enter desired format as second argument ("both" or "geojson" or "geopackage").
Output will be generated in ./buildings.

GeoJSON format will include only the buildings that have the exact address. Geopackage will have all buildings within 1000 meters of address centroid.

RUN EXAMPLE: 
python script.py "77 Excelsior Ave, Saratoga Springs, NY 12866" "both"


*I have not figured out a way to run outside of Anaconda Prompt. Not sure if that's good or bad yet, just a heads up.*


Citations:
Boeing, G. 2017. OSMnx: New Methods for Acquiring, Constructing, Analyzing, and Visualizing Complex Street Networks. Computers, Environment and Urban Systems 65, 126-139. doi:10.1016/j.compenvurbsys.2017.05.004
