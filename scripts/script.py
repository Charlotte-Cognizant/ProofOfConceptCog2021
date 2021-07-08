import osmnx as ox
import os
import sys
import json
import math

#create building directory if doesn't already exist
if not os.path.exists("buildings"):
    os.mkdir("buildings")

#address recieved from web api
address = str(sys.argv[1])

# specify that we're retrieving building footprint geometries
tags = {"building": True}

#distance buffer from address in meters
dist = 1000

#returns geopandas.GeoDataFrame
gdf = ox.geometries_from_address(address, tags, dist)

#project gdf to UTM for which address centroid lies
gdf_proj = ox.project_gdf(gdf)

#filepath to save geopackage
fp = f"./buildings/{address}"

gdf_save = gdf.applymap(lambda x: str(x) if isinstance(x, list) else x)

#file format to save footprints as- "geopackage" or "geojson" or "both"
ff = str(sys.argv[2])

#filter out json features that do not correspond to the address
def filter_buildings(input_json, num, name):
    with open(input_json) as jsonfile:
        gj = json.load(jsonfile)
    gj['features'] = [add for add in gj['features'] if (add['properties']['addr:housenumber'] == num and add['properties']['addr:street'] == name)]
    with open(input_json, 'w') as f:
        json.dump(gj, f)


#format address to be used with OSM
add_split = address.split(",")
add_part = add_split[0].partition(" ")
add_num = add_part[0]
add_name = add_part[2]
add_name_suffix = add_name.split()[-1].capitalize()
add_name_prefix = add_name.rsplit(' ', 1)[0].title()

if add_name_suffix == "Ave" or add_name_suffix == "Av":
    add_name_suffix = "Avenue"
if add_name_suffix == "Aly" or add_name_suffix == "Ally":
    add_name_suffix = "Alley"
if add_name_suffix == "Blvd" or add_name_suffix == "Boul":
    add_name_suffix = "Boulevard"
if add_name_suffix == "Cswy":
    add_name_suffix = "Causeway"
if add_name_suffix == "Ctr" or add_name_suffix == "Cen":
    add_name_suffix = "Center"
if add_name_suffix == "Cir" or add_name_suffix == "Circ":
    add_name_suffix = "Circle"
if add_name_suffix == "Ct":
    add_name_suffix = "Court"
if add_name_suffix == "Cv":
    add_name_suffix = "Cove"
if add_name_suffix == "Xing" or add_name_suffix == "Crssng":
    add_name_suffix = "Crossing"
if add_name_suffix == "Dr" or add_name_suffix == "Drv":
    add_name_suffix = "Drive"
if add_name_suffix == "Exp" or add_name_suffix == "Expr":
    add_name_suffix = "Expressway"
if add_name_suffix == "Ext" or add_name_suffix == "Extn":
    add_name_suffix = "Extension"
if add_name_suffix == "Fwy" or add_name_suffix == "Frwy":
    add_name_suffix = "Freeway"
if add_name_suffix == "Ave" or add_name_suffix == "Av":
    add_name_suffix = "Avenue"
if add_name_suffix == "Hwy" or add_name_suffix == "Hiway":
    add_name_suffix = "Highway"
if add_name_suffix == "Jct" or add_name_suffix == "Jctn":
    add_name_suffix = "Junction"
if add_name_suffix == "Ln":
    add_name_suffix = "Lane"
if add_name_suffix == "Pkwy" or add_name_suffix == "Pky":
    add_name_suffix = "Parkway"
if add_name_suffix == "Plza" or add_name_suffix == "Plz":
    add_name_suffix = "Plaza"
if add_name_suffix == "Rd":
    add_name_suffix = "Road"
if add_name_suffix == "Rte":
    add_name_suffix = "Route"
if add_name_suffix == "Sq" or add_name_suffix == "Sqr":
    add_name_suffix = "Square"
if add_name_suffix == "St" or add_name_suffix == "Str":
    add_name_suffix = "Street"
if add_name_suffix == "Ter" or add_name_suffix == "Terr":
    add_name_suffix = "Terrace"
if add_name_suffix == "Trwy":
    add_name_suffix = "Throughway"
if add_name_suffix == "Trl":
    add_name_suffix = "Trail"
if add_name_suffix == "Tpke":
    add_name_suffix = "Turnpike"
if add_name_suffix == "Wy":
    add_name_suffix = "Way"

add_name_full = add_name_prefix + " " + add_name_suffix


#save file as indicated format
if ff == "geopackage":
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.gpkg", driver="GPKG")
if ff == "geojson":
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.json", driver="GeoJSON")
    filter_buildings(f"{fp}.json",add_num, add_name_full)
if ff == "both":
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.gpkg", driver="GPKG")
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.json", driver="GeoJSON")
