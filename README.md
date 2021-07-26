
# Berechnungen verschiedener Wetterwerte

Eingabe von Lufttemperatur in °Celsius, rel. Luftfeuchte in %, Windgeschwindigkeit in m/s.
### 1. Initialisieren der Klasse WeatherCalc

WeatherCalc.InitWeatherCalcCelsius(TemperaturInCelsius, RelativeLuftFeuchteInProzent, WindgeschwindigkeitMeterProSekunde);

... (oder mit Lufttemperatur in Kelvin: WeatherCalc.InitWeatherCalcKelvin(...) )
### 2. Alle Werte berechnen

WeatherCalc.CalculateAll(); (Bei Initialisierung nicht nötig.)
### 3. Abfrage der berechneten Werte

Beispiel Konsolenanwendung:


WeatherCalc.InitWeatherCalcCelsius(33,75,33);
            
Console.WriteLine("Temperatur in [°K]: " + WeatherCalc.luftTempK);

Console.WriteLine("Temperatur in [°C]: " + WeatherCalc.luftTempC);

Console.WriteLine("Sättigungs-Dampfdruck [hPa] für Taupunkt: " + WeatherCalc.SDDbyC4T);

Console.WriteLine("Sättigungs-Dampfdruck [hPa] für Frostpunkt: " + WeatherCalc.SDDbyC4F);

Console.WriteLine("Dampfdruck in [hPa] für Taupunkt: " + WeatherCalc.DDbyCplusRelLF4T);

Console.WriteLine("Dampfdruck in [hPa] für Frostpunkt: " + WeatherCalc.DDbyCplusRelLF4F);


Console.WriteLine("Taupunkt in [°C]: " + WeatherCalc.tauPunktC);

Console.WriteLine("Frostpunkt in [°C]: " + WeatherCalc.frostPunktC);

Console.WriteLine("Wolkenhöhe in [m]: " + WeatherCalc.wolkenUnterGrenzeM);


Console.WriteLine("Hitzeindex in [°C]: " + WeatherCalc.hitzeIndex);

Console.WriteLine("Windchill in [°C]: " + WeatherCalc.windChillTemp);


Console.WriteLine("Absolute Feuchte [g Wasserdampf pro m3 Luft]: " + WeatherCalc.absLuftFeuchtGpCm);


Ausgabe:

Initalisieren der Wetter-Toolbox mit Messwerten [°C][%][m/s]

Berechnen aller Daten anhand gegebener Werte.

Temperatur in [°K]: 306,15

Temperatur in [°C]: 33

Sättigungs-Dampfdruck [hPa] für Taupunkt: 50,29607398872651

Sättigungs-Dampfdruck [hPa] für Frostpunkt: 6,1078

Dampfdruck in [hPa] für Taupunkt: 37,722055491544886

Dampfdruck in [hPa] für Frostpunkt: 4,58085

Taupunkt in [°C]: 27,966539941899494

Frostpunkt in [°C]: -0

Wolkenhöhe in [m]: 629,1825072625631

Hitzeindex in [°C]: 46,07895875000006

Windchill in [°C]: 36,62935124595018

Absolute Feuchte [g Wasserdampf pro m3 Luft]: 27,02395437738577

        
