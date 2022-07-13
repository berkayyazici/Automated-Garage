# Automated-Garage
Automated Garage System API

This is a parking system. You can park your car (POST) and check information about your spot name (e.g. F4), when did you park, what is your ticket number and which floor your car is on via your ticket number. Ticker number are generated randomly, so no-one has same ticket number. When you want to leave, you can do it with your plate number (We can do it via ticket number, but I wanted to do it with different parameter.). Also, plate numbers must be unique, so if anyone try to park his/her car with a plate number that have been already parked, they will get a warning.

Cars have 3 different sizes; small, medium and large. There are a certain number of spots for these sizes; for small cars => 50 spots, for medium cars => 100 spots, for large cars => 30 spots.

If any of these spots are full, you cannot park your car.

Note : Spot numbers reset every time that we run app. I believe you can fix that bug :) 
