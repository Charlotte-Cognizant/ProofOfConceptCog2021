Instructions for insrtalling OSMnx and documentation: https://osmnx.readthedocs.io/en/stable/

Enter any valid address string as first cmd argument. 
Enter desired format as second argument ("both" or "geojson" or "geopackage").
Output geojson will be generated in ./buildings.
Output image will be generated in ./imagery.

GeoJSON format will include only the buildings that have the exact address. Geopackage will have all buildings within 500 meters of address centroid.

RUN EXAMPLE: 
python script.py "77 Excelsior Ave, Saratoga Springs, NY 12866" geojson

OUTPUT EXAMPLE:
./buildings/77excelsioravesaratogaspringsny12866.json
./imagery/77excelsioravesaratogaspringsny12866.png


Citations:
Boeing, G. 2017. OSMnx: New Methods for Acquiring, Constructing, Analyzing, and Visualizing Complex Street Networks. Computers, Environment and Urban Systems 65, 126-139. doi:10.1016/j.compenvurbsys.2017.05.004
