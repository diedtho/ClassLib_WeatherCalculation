using System;
using ClassLib_WeatherCalculation;

namespace ClassLib_WeatherCalculation
{
    /// <summary>
    /// Benötigte Konstanten
    /// </summary>
    static class Consts
    {
        /// <summary>
        /// Konstante 'Molekulargewicht des Wasserdampfes' in [kg/kmol].
        /// </summary>
        public const double mGWD = 18.016;
        

        /// <summary>
        /// Konstante 'universelle Gaskonstante' in [J/(kmol*K)].
        /// </summary>
        public const double univGK = 8214.3;

        public static readonly double[] c = new double[]
        {
            -8.785,
            1.611,
            2.339,
            -0.146,
            -1.231e-2,
            -1.642e-2,
            2.212e-3,
            7.255e-4,
            -3.582e-6
        };
    }
}

/// <summary>
/// Verschiedene Methoden für Wetterdatenberechnungen
/// </summary>
public static class WeatherCalc
{
    /// <summary>
    /// Variable 'Lufttemperatur' in [°C].
    /// </summary>
    public static double luftTempC { get; set; }

    /// <summary>
    /// Variable 'Lufttemperatur' in [°K].
    /// </summary>
    public static double luftTempK { get; set; }

    /// <summary>
    /// Variable 'absolute Luftfeuchtigkeit' in [g/m3].
    /// </summary>

    public static double absLuftFeuchtGpCm { get; set; }

    /// <summary>
    /// Variable 'relative Luftfeuchtigkeit' in [%].
    /// </summary>

    public static double relLuftFeuchtProz { get; set; }

    /// <summary>
    /// Variable 'Windgeschwindigkeit' in [m/sec].
    /// </summary>
    public static double windGeschwMpS { get; set; }
    
    /// <summary>
    /// Variable 'Windgeschwindigkeit' in [km/h].
    /// </summary>
    public static double windGeschwKmpH { get; set; }

    /// <summary>
    /// Variablen für Magnus-Formel-Parameter
    /// </summary>
    private static double aLW, bLW, aE, bE ;

    /// <summary>
    /// Variable 'Taupunkt' in [°C].
    /// </summary>
    public static double tauPunktC { get; set; }
    
    /// <summary>
    /// Variable 'Frostpunkt' in [°C].
    /// </summary>
    public static double frostPunktC { get; set; }

    /// <summary>
    /// Variable 'Spread' in [°C].
    /// </summary>
    public static double spreadC { get; set; }

    /// <summary>
    /// Variable 'Wolkenuntergrenze' in [m].
    /// </summary>
    public static double wolkenUnterGrenzeM { get; set; }

    /// <summary>
    /// Variable 'WindchillTemperatur' in [°C].
    /// </summary>
    public static double windChillTemp { get; set; }

    /// <summary>
    /// Variable 'Sättigungsdampfdruck für Taupunkt' aus Temperatur in [°C].
    /// </summary>
    public static double SDDbyC4T { get; set; }
    
    /// <summary>
    /// Variable 'Sättigungsdampfdruck für Frostpunkt' aus Temperatur in [°C].
    /// </summary>
    public static double SDDbyC4F { get; set; }
    

    /// <summary>
    /// Variable 'Sättigungsdampfdruck' aus Temperatur in [°C] und rel. Luftfeuchte in [%].
    /// für Taupunkt
    /// </summary>
    public static double DDbyCplusRelLF4T { get; set; }
    
    /// <summary>
    /// Variable 'Sättigungsdampfdruck' aus Temperatur in [°C] und rel. Luftfeuchte in [%].
    /// für Frostpunkt
    /// </summary>
    public static double DDbyCplusRelLF4F { get; set; }

    /// <summary>
    /// Variable 'Hitzeindex' in [°C].
    /// </summary>
    public static double hitzeIndex { get; set; }

    /// <summary>
    /// Methode 'Berechnen von WCT (Windchilltemperatur)' in [°C].
    /// </summary>
    static void calcWindChillTemp()
    {
        windChillTemp = 13.12 + 0.6215 * luftTempC + (0.3965 * luftTempC - 11.37)
            * Math.Pow(windGeschwMpS, 0.16);
    }

    /// <summary>
    /// Methode 'Temperaturumrechnung in [°C] zu [°K].
    /// </summary>
    static void calcC2K()
    {
        luftTempK = luftTempC + 273.15;
    }

    /// <summary>
    /// Methode 'Temperaturumrechnung in [°K] zu [°C].
    /// </summary>
    static void calcK2C()
    {
        luftTempC = luftTempK - 273.15;
    }
    
    /// <summary>
    /// Methode 'Umrechnung Windgewschwindigket [m/sec] -> [km/h]'.
    /// </summary>
    static void calcWindgeschwMpS2KmpH()
    {
        windGeschwKmpH = windGeschwMpS / 1000 * 360;
    }

    /// <summary>
    /// Methode 'Berechnung des Sättingsdampfdrucks aus Temperatur in [°C]'.
    /// </summary>
    static void calcSDDbyC()
    {
        double expoF = 0.0;
        double expoT = 0.0;

        if (luftTempC >= 0) // Sättigungsdampfdruck
        {
            aLW = 7.5;
            bLW = 237.3;
            expoT = aLW * luftTempC / (bLW + luftTempC);
        }
        else if (luftTempC < 0) // Sättigungsdampfdruck über Wasser (Taupunkt)
        {
            aLW = 7.6;
            bLW = 240.7;
            expoT = (aLW * luftTempC) / (bLW + luftTempC);
            aE = 9.5;
            bE = 265.5;
            expoF = (aE * luftTempC) / (bE + luftTempC);
        }

        SDDbyC4T = 6.1078 * Math.Pow(10, expoT); // Sättigungsdampfdruck für Taupunktberechnung
        SDDbyC4F = 6.1078 * Math.Pow(10, expoF); // Sättigungsdampfdruck für Frostpunktberechnung
    }

    /// <summary>
    /// Methode 'Berechnung des Dampfdrucks aus Sättigungsdampfdruck + rel. Luftfeuchte'
    ///  für Taupunktberechnung
    /// </summary>
    static void calcDDbyTplusRelLF4T()
    {
        DDbyCplusRelLF4T = (relLuftFeuchtProz / 100) * SDDbyC4T;
    }
    
    /// <summary>
    /// Methode 'Berechnung des Dampfdrucks aus Sättigungsdampfdruck + rel. Luftfeuchte'
    ///  für Frostunktberechnung
    /// </summary>
    static void calcDDbyTplusRelLF4F()
    {
        DDbyCplusRelLF4F = (relLuftFeuchtProz / 100) * SDDbyC4F;
    }

    /// <summary>
    /// Methode 'Berechnung des Taupunkts in [°C]'.
    /// </summary>
    static void calcTaupunkt()
    {
        double v = Math.Log10(DDbyCplusRelLF4T / 6.1078);
        tauPunktC = bLW * v / (aLW - v);
    }
    
    /// <summary>
    /// Methode 'Berechnung des Frostpunkts in [°C]'.
    /// </summary>
    static void calcFrostpunkt()
    {
        double v = Math.Log10(DDbyCplusRelLF4F / 6.1078);
        frostPunktC = bE * v / (aE - v);

    }

    /// <summary>
    /// Methode 'Berechnung der absoluten Luftfeuchte'.
    /// </summary>
    static void calcAbsLF()
    {
        // Wasserdampfdichte bzw. absolute Feuchte (g/m3)
        absLuftFeuchtGpCm = Math.Pow(10, 5) * (Consts.mGWD / Consts.univGK) * (DDbyCplusRelLF4T/luftTempK);
    }


    /// <summary>
    /// Methode 'Berechnung des Spreads in [°C]'.
    /// </summary>
    static void calcSpreadC()
    {
        spreadC = luftTempC - tauPunktC;
    }

    /// <summary>
    /// Methode 'Berechnung der Wolkenuntergrenze in [m]'.
    /// </summary>
    static void calcWolkenuntergrenzeM()
    {
        wolkenUnterGrenzeM = spreadC * 125;
    }

    /// <summary>
    /// Methode 'Berechnung des Hitzeindex'.
    /// </summary>
    static void calcHitzeIndex()
    {
        hitzeIndex = Consts.c[0] + Consts.c[1] * luftTempC + Consts.c[2] * relLuftFeuchtProz +
                     Consts.c[3] * luftTempC * relLuftFeuchtProz + Consts.c[4] * luftTempC * luftTempC +
                     Consts.c[5] * relLuftFeuchtProz * relLuftFeuchtProz +
                     Consts.c[6] * luftTempC * luftTempC * relLuftFeuchtProz +
                     Consts.c[7] * luftTempC * relLuftFeuchtProz * relLuftFeuchtProz +
                     Consts.c[8] * luftTempC * luftTempC * relLuftFeuchtProz * relLuftFeuchtProz;
    }

    /// <summary>
    /// /// Initialisierung der Klasse:
    /// Lufttemperatur in Celsius
    /// und relative Luftfeuchte müssen mindestens angegeben werden
    /// </summary>
    /// <param name="luftTempCIn"></param>
    /// <param name="relLf"></param>
    /// <param name="windGeschwMpSec"></param>
    public static void InitWeatherCalcCelsius(double luftTempCIn, double relLf, double windGeschwMpSec)
    {
        Console.WriteLine("Initalisieren der Wetter-Toolbox mit Messwerten [°C][%][m/s]");
        luftTempC = luftTempCIn;
        relLuftFeuchtProz = relLf;
        windGeschwMpS = windGeschwMpSec;
        calcC2K();
        CalculateAll();
    }

    /// <summary>
    /// Initialisierung der Klasse:
    /// Lufttemperatur in Kelvin
    /// und relative Luftfeuchte müssen mindestens angegeben werden
    /// </summary>
    /// <param name="luftTempKIn"></param>
    /// <param name="relLf"></param>
    /// <param name="windGeschwMpSec"></param>
    public static void InitWeatherCalcKelvin(double luftTempKIn, double relLf, double windGeschwMpSec)
    {
        Console.WriteLine("Initalisieren der Wetter-Toolbox mit Messwerten [°K][%][m/s]");
        luftTempK = luftTempKIn;
        relLuftFeuchtProz = relLf;
        windGeschwMpS = windGeschwMpSec;
        calcK2C();
        CalculateAll();
    }

    /// <summary>
    /// Alle Berechnungen in richtiger Reihenfolge durchführen.
    /// </summary>
    public static void CalculateAll()
    {
        Console.WriteLine("Berechnen aller Daten anhand gegebener Werte.");
        calcWindgeschwMpS2KmpH();
        calcSDDbyC();
        calcDDbyTplusRelLF4T();
        calcDDbyTplusRelLF4F();
        calcTaupunkt();
        calcFrostpunkt();
        calcAbsLF();
        calcSpreadC();
        calcWolkenuntergrenzeM();
        calcHitzeIndex();
        calcWindChillTemp();
    }
}