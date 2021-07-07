import osmnx as ox
import os
import sys


#create building directory if doesn't already exist
if not os.path.exists("buildings"):
    os.mkdir("buildings")

#address recieved from web api
address = str(sys.argv[1])

# specify that we're retrieving building footprint geometries
tags = {"building": True}

#distance buffer from address in meters
dist = 500

#returns geopandas.GeoDataFrame
gdf = ox.geometries_from_address(address, tags, dist)

#project gdf to UTM for which address centroid lies
gdf_proj = ox.project_gdf(gdf)

#filepath to save geopackage
fp = f"./buildings/{address}"

gdf_save = gdf.applymap(lambda x: str(x) if isinstance(x, list) else x)

#file format to save footprints as- "geopackage" or "geojson" or "both"
ff = str(sys.argv[2])

#save file as indicated format
if ff == "geopackage":
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.gpkg", driver="GPKG")
if ff == "geojson":
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.json", driver="GeoJSON")
if ff == "both":
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.gpkg", driver="GPKG")
    gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.json", driver="GeoJSON")
