import osmnx as ox
import os
import sys
import json
import math
from datetime import datetime
from area import area
from shapely.geometry import shape
from shapely.geometry import LineString


#returns false if no buildings detected at address
def check_for_buildings(input_json):
    with open(input_json) as jsonfile:
        gj = json.load(jsonfile)
    if len(gj['features']) == 0:
        return False
    else:
        return True

#filter out json features that do not correspond to the address
def filter_buildings(input_json, num, name):
    with open(input_json) as jsonfile:
        gj = json.load(jsonfile)
    try:
        gj['features'] = [add for add in gj['features'] if (add['properties']['addr:housenumber'] == num and add['properties']['addr:street'] == name)]
    except KeyError:
        print ("Sorry, address number not available at this time")
        quit()
    with open(input_json, 'w') as f:
        json.dump(gj, f)

#add centroid, perimeter, area to json
def calc_geoms(input_json):
    with open(input_json) as jsonfile:
        geoJson = json.load(jsonfile)

    #calculate total area of buildings
    i = 0
    area_m2 = 0
    for f in geoJson['features']:
        area_m2 += area(geoJson['features'][i]['geometry'])
        i += 1

    #calculate centroid of buidings
    features = geoJson["features"]
    cents = []
    for feature in features:
        s = shape(feature["geometry"])
        cents.append(s.centroid)

    if len(cents) > 1:
        centroid = LineString(cents).centroid
    else:
        centroid = cents[0]

    centroid_lat = centroid.y
    centroid_lon = centroid.x

    #add area and centroid to geoJSON
    for feat in features:
        feat['properties']['total_area'] = area_m2
        feat['properties']['centroid_lat'] = centroid_lat
        feat['properties']['centroid_lon'] = centroid_lon

    with open(input_json, 'w') as f:
        json.dump(geoJson, f)

#set style of footprints
def set_style(input_json):
    #open geoJSON
    with open(input_json) as jsonfile:
        geoJson = json.load(jsonfile)
    #set style
    for f in geoJson['features']:
        f['properties']['stroke'] = '#7fff00'
    #write geojson
    with open(input_json, 'w') as f:
        json.dump(geoJson, f)

#add mm/dd/yyyy to properties
def add_date(input_json):
    #open geoJSON
    with open(input_json) as jsonfile:
        geoJson = json.load(jsonfile)

    now = datetime.now()

    # dd/mm/YY H:M:S
    dt_string = now.strftime("%m/%d/%Y %H:%M:%S")

    for f in geoJson['features']:
        f['properties']['dateRequested'] = dt_string
    #write geojson
    with open(input_json, 'w') as f:
        json.dump(geoJson, f)

#retrieve static map image using mapbox API
def mapbox_request(input_json):
    #mapbox token
    api_key = "pk.eyJ1IjoiaGFydGMxNyIsImEiOiJja3IyNWxmMGQyODZyMnB0OXJlOHd4ZGJrIn0.2abXKt7EfUNNHWzvj6buRg"
    api_auth = "set MAPBOX_ACCESS_TOKEN=" + api_key
    api_init = "mapbox ..."

    #open geoJSON
    with open(input_json) as jsonfile:
        geoJson = json.load(jsonfile)

    #address coordinates
    lon = geoJson['features'][0]['properties']['centroid_lon']
    lat = geoJson['features'][0]['properties']['centroid_lat']

    #mapbox parameters
    zoom = '15'
    size = '800 800'
    basemap = 'mapbox.satellite'
    out_image = f"./imagery/{input_json[12:-5]}.png"

    #api request string
    rq = f"mapbox staticmap --features {input_json} {basemap} {out_image}"

    #make requests
    #os.system(api_auth)
    #os.system(api_init)
    os.system(rq)



#format address to be used with OSM
def format_address(address):
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

    return [add_num, add_name_full]



def main():

    #create building directory if doesn't already exist
    if not os.path.exists("buildings"):
        os.mkdir("buildings")

    #create imagery directory if doesn't already exist
    if not os.path.exists("imagery"):
        os.mkdir("imagery")

    #address recieved from web api
    address = str(sys.argv[1])
    print (address)
    # specify that we're retrieving building footprint geometries
    tags = {"building": True}

    #distance buffer from address in meters
    dist = 500

    #returns geopandas.GeoDataFrame
    try:
        gdf = ox.geometries_from_address(address, tags, dist)
    except ValueError:
        print ("Address cannot be found.")
        quit()

    #project gdf to UTM for which address centroid lies
    gdf_proj = ox.project_gdf(gdf)

    #filepath to save building footprint
    fp = f"./buildings/{address}"

    gdf_save = gdf.applymap(lambda x: str(x) if isinstance(x, list) else x)

    #file format to save footprints as- "geopackage" or "geojson" or "both"
    ff = str(sys.argv[2])

    #file name of geojson
    geojson_fn = f"{fp}.json".replace(" ", "").replace(",", "").lower()

    #save file as indicated format
    if ff == "geopackage":
        gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.gpkg", driver="GPKG")
    if ff == "geojson":
        print ("check2")
        gdf_save.drop(labels="nodes", axis=1).to_file(geojson_fn, driver="GeoJSON")
        filter_buildings(geojson_fn, format_address(address)[0], format_address(address)[1])
        if check_for_buildings(geojson_fn) == True:
            print ("check1")
            calc_geoms(geojson_fn)
            set_style(geojson_fn)
            add_date(geojson_fn)
            mapbox_request(geojson_fn)
        else:
            print ("No building detected at given address.")
            os.remove(geojson_fn)
    if ff == "both":
        gdf_save.drop(labels="nodes", axis=1).to_file(f"{fp}.gpkg", driver="GPKG")
        gdf_save.drop(labels="nodes", axis=1).to_file(geojson_fn, driver="GeoJSON")

    #open geoJSON
    #with open(geojson_fn) as jsonfile:

        #geoJson = json.load(jsonfile)
    print (address)



if __name__ == "__main__":
    main()
