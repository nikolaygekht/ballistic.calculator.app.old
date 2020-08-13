/**
 * Created by vvalitsky on 24.10.15.
 */

BallisticCalculator = function() {

    var b = this;

    /************Constants and Enums*************/

    b.reportsUnits = {
        Imperial : 'imperial',
        Metric : 'metric'
    };

    Object.freeze(b.reportsUnits);

    b.angleUnits = {
        Radian : "radian",
        Degree : "degree",
        Moa : "moa",
        Mil : "mil",
        MilDot : "mil_dot",
        AMil : "amil",
        InPer100Yards : "in_per_100_yards",
        CmPer100Meters : "cm_per_100_meters"
    };

    Object.freeze(b.angleUnits);

    b.distanceUnits = {
        Inch : "inch",
        Foot : "foot",
        Yard : "yard",
        Mile : "mile",
        Millimeter : "millimeter",
        Centimeter : "centimeter",
        Meter : "meter",
        Kilometer : "kilometer"
    };

    Object.freeze(b.distanceUnits);

    b.energyUnits = {
        Joule: "joule",
        FootPounds: "foot_pounds"
    };

    Object.freeze(b.energyUnits);

    b.pressureUnits = {
        MmHg : "mm_hg",
        InchHg : "inch_hg",
        Bar : "bar"
    };

    Object.freeze(b.pressureUnits);

    b.temperatureUnits = {
        Celsius : "celsius",
        Fahrenheit : "fahrenheit"
    };

    Object.freeze(b.temperatureUnits);

    b.velocityUnits = {
        MeterPerSecond : "meter_per_second",
        KilometersPerHour : "kilometers_per_hour",
        FeetPerSecond : "feet_per_second",
        MilesPerHour : "miles_per_hour",
        Millimeter : "millimeter"
    };

    Object.freeze(b.velocityUnits);

    b.weightUnits = {
        Grain : "grain",
        Gram : "gram",
        Pound : "pound",
        Kilogram : "kilogram"
    };

    Object.freeze(b.weightUnits);

    b.dragTable = {
        G1 : 'g1',
        G2 : 'g2',
        G5 : 'g5',
        G6 : 'g6',
        G7 : 'g7',
        G8 : 'g8',
        GL : 'gl',
        GI : 'gi'
    };

    Object.freeze(b.dragTable);

    b.trajectoryTarget = {
        ElevationAngle : 'elevationAngle',
        ElevationAngleAndRange : 'elevationAngleAndRange',
        Range : 'range',
        DangerZone : 'dangerZone'
    };

    Object.freeze(b.trajectoryTarget);


    /*************Units************/

    /*********************Angle Unit******************/
    b.angle = function(value, unit) {
        var own = this;

        own.value = null;
        own.unit = null;

        if (!checkNumeric(value)) throw "Was passed undefined value";
        var parsedUnit = parseUnit(unit, b.angleUnits);
        if (!check(parsedUnit)) throw "Was passed not Angle unit type";


        /****************Methods**************/
        own.toUnit = function(unit) {
            return new b.angle(own.get(unit), unit);
        };

        own.get = function(unit) {
            var parsedUnit = parseUnit(unit, b.angleUnits);

            if (!check(parsedUnit)) throw "Was passed not Angle unit type";
            switch (unit) {
                case b.angleUnits.Radian:
                    return own.value;
                case b.angleUnits.Degree:
                    return own.value * 180 / Math.PI;
                case b.angleUnits.MilDot:
                    return own.value * 3200 / Math.PI;
                case b.angleUnits.AMil:
                    return own.value * 3000 / Math.PI;
                case b.angleUnits.Moa:
                    return own.value * 60 * 180 / Math.PI;
                case b.angleUnits.Mil:
                    return own.value * 1000;
                case  b.angleUnits.InPer100Yards:
                    return Math.tan(own.value) * 300 * 12;
                case b.angleUnits.CmPer100Meters:
                    return Math.tan(own.value) * 100 * 100;
                default :
                    throw "Unknown unit";
            }
        };

        own.set = function(value, unit) {
            var parsedUnit = parseUnit(unit, b.angleUnits);
            if (!check(parsedUnit)) throw "Was passed not Angle unit type";
            if (!checkNumeric(value)) throw "Was passed undefined value";

            own.unit = unit;

            switch (parsedUnit) {
                case b.angleUnits.Radian:
                    own.value = value;
                    break;
                case b.angleUnits.Degree:
                    own.value = value * Math.PI / 180;
                    break;
                case b.angleUnits.MilDot:
                    own.value = value * Math.PI / 3200;
                    break;
                case b.angleUnits.AMil:
                    own.value = value * Math.PI / 3000;
                    break;
                case b.angleUnits.Moa:
                    own.value = value / 60 * Math.PI / 180;
                    break;
                case b.angleUnits.Mil:
                    own.value = value / 1000;
                    break;
                case b.angleUnits.InPer100Yards:
                    own.value = Math.atan(value / 12 / 300);
                    break;
                case b.angleUnits.CmPer100Meters:
                    own.value = Math.atan(value / 100 / 100);
                    break;
                default:
                    throw "Unknown unit";
            }
        };
        own.set(value, parsedUnit);
    };

     function angleLogic() {
         var own = this;

         own.converter = new b.angle(0, b.angleUnits.Radian);

         own.convert = function(value, unitFrom, unitTo) {
             own.converter.set(value, unitFrom);
             return own.converter.get(unitTo);
         };

         own.getDefaultDisplayPrecision = function(unit) {
            var parsedUnit = parseUnit(unit, b.angleUnits);
            if (!check(parsedUnit)) throw "Was passed not Angle unit type";
            switch (parsedUnit) {
                case b.angleUnits.Radian:
                    return 6;
                case b.angleUnits.Degree:
                    return 1;
                case b.angleUnits.Moa:
                    return 1;
                case b.angleUnits.Mil:
                    return 1;
                case b.angleUnits.MilDot:
                    return 1;
                case b.angleUnits.AMil:
                    return 1;
                case b.angleUnits.InPer100Yards:
                    return 1;
                default :
                    throw "Unknown unit";
            }
        };

        own.unitToName = function(parsedUnit) {
            switch (parsedUnit) {
                case b.angleUnits.Radian:
                    return "rad";
                case b.angleUnits.Degree:
                    return "°";
                case b.angleUnits.Moa:
                    return "moa";
                case b.angleUnits.Mil:
                    return "mil";
                case b.angleUnits.AMil:
                    return "am";
                case b.angleUnits.MilDot:
                    return "mdot";
                case b.angleUnits.InPer100Yards:
                    return "in/100y";
                case b.angleUnits.CmPer100Meters:
                    return "cm/100m";
                default:
                    throw "Unknown unit";
            }
        };

        own.nameToUnit = function(name) {
            if (!check(name)) throw "Was passed undefined name";
            if (name === "rad")
                return b.angleUnits.Radian;
            else if (name === "deg" || name === "°")
                return b.angleUnits.Degree;
            else if (name === "moa")
                return b.angleUnits.Moa;
            else if (name === "mil")
                return b.angleUnits.Mil;
            else if (name === "am")
                return b.angleUnits.AMil;
            else if (name === "mdot")
                return b.angleUnits.MilDot;
            else if (name === "in/100y")
                return b.angleUnits.InPer100Yards;
            else if (name === "cm/100m")
                return b.angleUnits.CmPer100Meters;
            throw "Unknown unit";
        };

         own.getDefaultUnit = function() {
             return b.angleUnits.Radian;
         };

    }

    /********************Distance******************/
    b.distance = function(value, unit) {
        var own = this;

        own.value = null;
        own.unit = null;

        /******************Methods***************/
        own.set = function(value, unit) {
            var parsedUnit = parseUnit(unit, b.distanceUnits);
            if (!check(parsedUnit)) throw "Was passed not Distance unit type";
            if (!checkNumeric(value)) throw "Was passed undefined value";
            own.unit = unit;
            switch (unit) {
                case b.distanceUnits.Inch:
                    own.value = value;
                    break;
                case b.distanceUnits.Foot:
                    own.value = value * 12;
                    break;
                case b.distanceUnits.Yard:
                    own.value = value * 36;
                    break;
                case b.distanceUnits.Mile:
                    own.value = value * 63360;
                    break;
                case b.distanceUnits.Millimeter:
                    own.value = value / 25.4;
                    break;
                case b.distanceUnits.Centimeter:
                    own.value = value / 2.54;
                    break;
                case b.distanceUnits.Meter:
                    own.value = value / 25.4 * 1000;
                    break;
                case b.distanceUnits.Kilometer:
                    own.value = value / 25.4 * 1000000;
                    break;
            }
        };

        own.get = function(unit) {
            var parsedUnit = parseUnit(unit, b.distanceUnits);
            if (!check(parsedUnit)) throw "Was passed not Distance unit type";
            switch (parsedUnit) {
                case b.distanceUnits.Inch:
                    return own.value;
                case b.distanceUnits.Foot:
                    return own.value / 12;
                case b.distanceUnits.Yard:
                    return own.value / 36;
                case b.distanceUnits.Mile:
                    return own.value / 63360;
                case b.distanceUnits.Millimeter:
                    return own.value * 25.4;
                case b.distanceUnits.Centimeter:
                    return own.value * 2.54;
                case b.distanceUnits.Meter:
                    return own.value * 25.4 / 1000;
                case b.distanceUnits.Kilometer:
                    return own.value * 25.4 / 1000000;
                default :
                    throw "Unknown unit";
            }
        };

        own.toUnit = function(unit) {
            return b.distance(own.get(unit), unit);
        };

        own.toUnit = function(unit, precision) {
            if (!checkNumeric(precision)) throw "Passed precision is incorrect";
            return new b.distance(toFixed(own.get(unit), precision), unit);
        };

        own.set(value, unit);

    };

   function distanceLogic() {
        var own = this;

        own.converter = new b.distance(0, b.distanceUnits.Inch);

        /**********************Methods********************/

        own.convert = function(value, unitFrom, unitTo) {
            own.converter.set(value, unitFrom);
            return own.converter.get(unitTo);
        };

        own.nameToUnit = function(name) {
            if (!check(name)) throw "Was passed undefined name";
            if (name === "in" || name === "\"")
                return b.distanceUnits.Inch;
             else if (name === "ft" || name === "\'")
                return b.distanceUnits.Foot;
            else if (name === "yd")
                return b.distanceUnits.Yard;
            else if (name === "mi")
                return b.distanceUnits.Mile;
            else if (name === "mm")
                return b.distanceUnits.Millimeter;
            else if (name === "cm")
                return b.distanceUnits.Centimeter;
            else if (name === "m")
                return b.distanceUnits.Meter;
            else if (name === "km")
                return b.distanceUnits.Kilometer;
            else
                throw "Unknown unit";
        };

        own.getDefaultDisplayPrecision = function(unit) {
            var parsedUnit = parseUnit(unit, b.distanceUnits);
            if (!check(parsedUnit)) throw "Was passed not Distance unit type";
            switch (parsedUnit) {
                case b.distanceUnits.Inch:
                    return 1;
                case b.distanceUnits.Foot:
                    return 2;
                case b.distanceUnits.Yard:
                    return 2;
                case b.distanceUnits.Mile:
                    return 3;
                case b.distanceUnits.Millimeter:
                    return 0;
                case b.distanceUnits.Centimeter:
                    return 1;
                case b.distanceUnits.Meter:
                    return 2;
                case b.distanceUnits.Kilometer:
                    return 3;
                default :
                    throw "Unknown unit";
            }
        };

        own.getDefaultUnit = function() {
            return b.distanceUnits.Inch;
        }
    }

    /*****************Energy*****************/
    b.energy = function(value, unit) {
        var own = this;

        own.value = null;
        own.unit = null;

        /********************Methods******************/

        own.set = function(value, unit) {
            var parsedUnit = parseUnit(unit, b.energyUnits);
            if (!check(parsedUnit)) throw "Was passed not Energy unit type";
            if (!checkNumeric(value)) throw "Was passed undefined value";
            own.unit = unit;
            switch (parsedUnit) {
                case b.energyUnits.FootPounds:
                    own.value = value;
                    break;
                case b.energyUnits.Joule:
                    own.value = value * 0.737562149277;
                    break;
            }
        };

        own.get = function(unit) {
            var parsedUnit = parseUnit(unit, b.energyUnits);
            if (!check(parsedUnit)) throw "Was passed not Energy unit type";
            switch (parsedUnit) {
                case b.energyUnits.FootPounds:
                    return own.value;
                case b.energyUnits.Joule:
                    return own.value / 0.737562149277;
                default :
                    throw "Unknown unit";
            }
        };

        own.toUnit = function(unit) {
            return new b.energy(own.get(unit), unit);
        };

        own.set(value, unit);
    };

    function energyLogic() {
        var own = this;
        own.converter = new b.energy(0, b.energyUnits.FootPounds);

        own.convert = function(value, unitFrom, unitTo) {
            own.converter.set(value, unitFrom);
            return own.converter.get(unitTo);
        };

        own.unitToName = function(unit) {
            var parsedUnit = parseUnit(unit, b.energyUnits);
            if (!check(parsedUnit)) throw "Was passed not Energy unit type";
            switch (parsedUnit) {
                case b.energyUnits.FootPounds:
                    return "ft·lb";
                case b.energyUnits.Joule:
                    return "J";
                default :
                    throw "Unknown unit";
            }
        };

        own.nameToUnit = function(name) {
            if (!check(name)) throw "Passed name is undefined";
            if (name === "J") {
                return b.energyUnits.Joule;
            }
            else if (name === "ft-lb" || name === "ft·lb") {
                return b.energyUnits.FootPounds;
            } else{
                throw "Unknown unit";
            }
        };

        own.getDefaultUnit = function() {
            return b.energyUnits.FootPounds;
        }
    }


    /*****************Pressure******************/
    b.pressure = function(value, unit) {
        var own = this;

        own.value = null;
        own.unit = null;

        own.set = function(value, unit) {
            var parsedUnit = parseUnit(unit, b.pressureUnits);
            if (!check(parsedUnit)) throw "Was passed not Pressure unit type";
            if (!checkNumeric(value)) throw "Was passed undefined value";
            own.unit = unit;
            switch (parsedUnit) {
                case b.pressureUnits.MmHg:
                    own.value = value;
                    break;
                case b.pressureUnits.InchHg:
                    own.value = value * 25.4;
                    break;
                case b.pressureUnits.Bar:
                    own.value = value * 750.061683;
                    break;
            }
        };

        own.get = function(unit) {
            var parsedUnit = parseUnit(unit, b.pressureUnits);
            if (!check(parsedUnit)) throw "Was passed not Pressure unit type";
            switch (parsedUnit) {
                case b.pressureUnits.MmHg:
                    return own.value;
                case b.pressureUnits.InchHg:
                    return own.value / 25.4;
                case b.pressureUnits.Bar:
                    return own.value / 750.061683;
                default :
                    throw "Unknown unit";
            }
        };

        own.toUnit = function(unit) {
            return new b.pressure(own.get(unit), unit);
        };

        own.set(value, unit);
    };

    function pressureLogic() {
        var own = this;

        own.converter = new b.pressure(0, b.pressureUnits.MmHg);

        /****************Methods***************/
        own.convert = function(value, unitFrom, unitTo) {
            own.converter.set(value, unitFrom);
            return own.converter.get(unitTo);
        };

        own.unitToName = function(unit) {
            var parsedUnit = parseUnit(unit, b.pressureUnits);
            if (!check(parsedUnit)) throw "Was passed not Pressure unit type";
            switch (parsedUnit) {
                case b.pressureUnits.MmHg:
                    return "mmHg";
                case b.pressureUnits.InchHg:
                    return "inHg";
                case b.pressureUnits.Bar:
                    return "bar";
                default :
                    throw "Unknown unit";
            }
        };

        own.nameToUnit = function(name) {
            if (!check(name)) throw "Was passed undefined name";
            if (name === "mmHg") {
                return b.pressureUnits.MmHg;
            } else if (name === "inHg") {
                return b.pressureUnits.InchHg;
            } else if (name === "bar") {
                return b.pressureUnits.Bar;
            } else {
                throw "Unknown unit";
            }
        };

        own.getDefaultDisplayPrecision = function(unit) {
            var parsedUnit = parseUnit(unit, b.pressureUnits);
            if (!check(parsedUnit)) throw "Was passed not Pressure unit type";

            switch (parsedUnit) {
                case b.pressureUnits.MmHg:
                    return 0;
                case b.pressureUnits.InchHg:
                    return 2;
                case b.pressureUnits.Bar:
                    return 4;
                default :
                    throw "Unknown unit";
            }
        };

        own.getDefaultUnit = function() {
            return b.pressureUnits.MmHg;
        };

    }



    /*************Temperature**********/
    b.temperature = function(value, unit) {
        var own = this;

        own.value = null;
        own.unit = null;

        /**********************Methods********************/

        own.convert = function(value, unitFrom, unitTo) {
            own.converter.set(value, unitFrom);
            return own.converter.get(unitTo);
        };

        own.set = function(value, unit) {
            var parsedUnit = parseUnit(unit, b.temperatureUnits);
            if (!check(parsedUnit)) throw "Was passed not Temperature unit type";
            if (!checkNumeric(value)) throw "Was passed undefined value";
            own.unit = unit;
            switch (unit) {
                case b.temperatureUnits.Fahrenheit:
                    own.value = value;
                    break;
                case b.temperatureUnits.Celsius:
                    own.value = value * 9 / 5 + 32;
                    break;
            }
        };

        own.get = function(unit) {
            var parsedUnit = parseUnit(unit, b.temperatureUnits);
            if (!check(parsedUnit)) throw "Was passed not Temperature unit type";
            switch (parsedUnit) {
                case b.temperatureUnits.Fahrenheit:
                    return own.value;
                case b.temperatureUnits.Celsius:
                    return (own.value - 32) * 5 / 9;
                default :
                    throw "Unknown unit";
            }
        };

        own.toUnit = function(unit) {
            return new b.temperature(own.get(unit), unit);
        };


        own.set(value, unit);
    };

    function  temperatureLogic() {
        var own = this;
        own.converter = new b.temperature(0, b.temperatureUnits.Fahrenheit);

        /****************Methods***************/
        own.convert = function(value, unitFrom, unitTo) {
            own.converter.set(value, unitFrom);
            return own.converter.get(unitTo);
        };

        own.unitToName = function(unit) {
            var parsedUnit = parseUnit(unit, b.temperatureUnits);
            if (!check(parsedUnit)) throw "Was passed not Temperature unit type";
            switch (parsedUnit) {
                case b.temperatureUnits.Fahrenheit:
                    return "°F";
                case b.temperatureUnits.Celsius:
                    return "°C";
                default :
                    throw "Unknown unit";
            }
        };

        own.nameToUnit = function(name) {
            if (!check(name)) throw "Passed name is undefined";
            if (name === "°F") {
                return b.temperatureUnits.Fahrenheit;
            } else if (name === "°C") {
                return b.temperatureUnits.Celsius;
            } else {
                throw "Unknown unit";
            }
        };

        own.getDefaultDisplayPrecision = function(unit) {
            var parsedUnit = parseUnit(unit, b.temperatureUnits);
            if (!check(parsedUnit)) throw "Was passed not Temperature unit type";
            return 0;
        };

        own.getDefaultUnit = function() {
            return b.temperatureUnits.Fahrenheit;
        }
    }


    /**************Velocity***************/
    b.velocity = function(value, unit) {
        var own = this;

        own.value = null;
        own.unit = null;

        own.set = function(value, unit) {
            var parsedUnit = parseUnit(unit, b.velocityUnits);
            if (!check(parsedUnit)) throw "Was passed not Velocity unit type";
            if (!checkNumeric(value)) throw "Was passed undefined value";
            own.unit = unit;

            switch (parsedUnit) {
                case b.velocityUnits.MeterPerSecond:
                    own.value = value;
                    break;
                case b.velocityUnits.KilometersPerHour:
                    own.value  = value / 3.6;
                    break;
                case b.velocityUnits.FeetPerSecond:
                    own.value  = value / 3.2808399;
                    break;
                case b.velocityUnits.MilesPerHour:
                    own.value  = value / 2.23693629;
                    break;
            }
        };

        own.get = function(unit) {
            var parsedUnit = parseUnit(unit, b.velocityUnits);
            if (!check(parsedUnit)) throw "Was passed not Velocity unit type";
                switch (parsedUnit) {
                    case b.velocityUnits.MeterPerSecond:
                        return own.value;
                    case b.velocityUnits.KilometersPerHour:
                        return own.value * 3.6;
                    case b.velocityUnits.FeetPerSecond:
                        return own.value * 3.2808399;
                    case b.velocityUnits.MilesPerHour:
                        return own.value * 2.23693629;
                    default :
                        throw "Unknown unit";
            }
        };

        own.toUnit = function(unit) {
            return new b.velocity(own.get(unit), unit);
        };

        own.set(value, unit);
    };

    function velocityLogic() {
        var own = this;
        own.converter = new b.velocity(0, b.velocityUnits.MeterPerSecond);

        /****************Methods***************/
        own.convert = function(value, unitFrom, unitTo) {
            own.converter.set(value, unitFrom);
            return own.converter.get(unitTo);
        };

        own.unitToName = function(unit) {
            var parsedUnit = parseUnit(unit, b.velocityUnits);
            if (!check(parsedUnit)) throw "Was passed not Velocity unit type";
            switch (parsedUnit) {
                case b.velocityUnits.MeterPerSecond:
                    return "m/s";
                case b.velocityUnits.KilometersPerHour:
                    return "km/h";
                case b.velocityUnits.FeetPerSecond:
                    return "ft/s";
                case b.velocityUnits.MilesPerHour:
                    return "mi/h";
                default :
                    throw "Unknown unit";
            }
        };

        own.nameToUnit = function(name) {
            if (!check(name)) throw "Passed name is undefined";
            if (name === "m/s") {
                return b.velocityUnits.MeterPerSecond;
            } else if (name === "km/h") {
                return b.velocityUnits.KilometersPerHour;
            } else if (name === "ft/s") {
                return b.velocityUnits.FeetPerSecond;
            } else if (name === "mi/h") {
                return b.velocityUnits.MilesPerHour;
            } else {
                throw "Unknown unit";
            }
        };

        own.getDefaultDisplayPrecision = function(unit) {
            var parsedUnit = parseUnit(unit, b.velocityUnits);
            if (!check(parsedUnit)) throw "Was passed not Velocity unit type";
            switch (unit) {
                case b.velocityUnits.MeterPerSecond:
                    return 0;
                case b.velocityUnits.KilometersPerHour:
                    return 1;
                case b.velocityUnits.FeetPerSecond:
                    return 0;
                case b.velocityUnits.MilesPerHour:
                    return 1;
                default :
                    throw "Unknown unit";
            }
        };

        own.getDefaultUnit = function(){
            return b.velocityUnits.MeterPerSecond;
        };

    }


    /****************Weight*****************/
    b.weight = function(value, unit) {
        var own = this;
        own.value = null;
        own.unit = null;

        own.set = function(value, unit) {
            var parsedUnit = parseUnit(unit, b.weightUnits);
            if (!check(parsedUnit)) throw "Was passed not Weight unit type";
            if (!checkNumeric(value)) throw "Was passed undefined value";

            own.unit  = unit;

            switch (parsedUnit) {
                case b.weightUnits.Grain:
                    own.value = value;
                    break;
                case b.weightUnits.Gram:
                    own.value = value * 15.4323584;
                    break;
                case b.weightUnits.Kilogram:
                    own.value = value * 1000 * 15.4323584;
                    break;
                case b.weightUnits.Pound:
                    own.value = value / 0.000142857143;
                    break;
            }
        };

        own.get = function(unit) {
            var parsedUnit = parseUnit(unit, b.weightUnits);
            if (!check(parsedUnit)) throw "Was passed not Weight unit type";
            switch (parsedUnit) {
                case b.weightUnits.Grain:
                    return own.value;
                case b.weightUnits.Gram:
                    return own.value / 15.4323584;
                case b.weightUnits.Kilogram:
                    return own.value / 15.4323584 / 1000;
                case b.weightUnits.Pound:
                    return own.value * 0.000142857143;
                default :
                    throw "Unknown unit";
            }
        };

        own.toUnit = function(unit) {
            return new b.weight(own.get(unit), unit);
        };

        own.set(value, unit);
    };

    function weightLogic() {
        var own = this;
        own.converter = new b.weight(0, b.weightUnits.Grain);

        /****************Methods***************/
        own.convert = function(value, unitFrom, unitTo) {
            own.converter.set(value, unitFrom);
            return own.converter.get(unitTo);
        };

        own.unitToName = function(unit){
            var parsedUnit = parseUnit(unit, b.weightUnits);
            if (!check(parsedUnit)) throw "Was passed not Weight unit type";
            switch (parsedUnit) {
                case b.weightUnits.Grain:
                    return "gr";
                case b.weightUnits.Gram:
                    return "g";
                case b.weightUnits.Kilogram:
                    return "kg";
                case b.weightUnits.Pound:
                    return "lb";
                default :
                    throw "Unknown unit";
            }
        };

        own.nameToUnit = function(name) {
            if (!check(name)) throw "Passed name is undefined";
            if (name === "gr") {
                return b.weightUnits.Grain;
            } else if (name === "g") {
                return b.weightUnits.Gram;
            } else if (name === "kg") {
                return b.weightUnits.Kilogram;
            } else if (name === "lb") {
                return b.weightUnits.Pound;
            } else {
                throw "Unknown unit";
            }
        };

        own.getDefaultDisplayPrecision = function(unit) {
            var parsedUnit = parseUnit(unit, b.weightUnits);
            if (!check(parsedUnit)) throw "Was passed not Weight unit type";
            switch (parsedUnit) {
                case b.weightUnits.Grain:
                    return 0;
                case b.weightUnits.Gram:
                    return 1;
                case b.weightUnits.Pound:
                    return 3;
                case b.weightUnits.Kilogram:
                    return 3;
                default :
                    throw "Unknown unit";
            }
        };

        own.getDefaultUnit = function() {
            return b.weightUnits.Grain;
        }


    }



    /*******************JBM*****************/

    /**
     * void function
     * instance of this functions is provide
     * configuring for atmo object init
     */
    b.atmoArguments = function() {
        var own = this;
        own.temperature = null;
        own.pressure = null;
        own.humidity = null;
        own.altitude = null;
    };

    /**
     * prototype
     * arguments should be instance of atmoArguments or null/undefined
     * all prototype field ara available as object property
     */
    b.atmo = function(atmoArgs) {
        var own = this;

        /**********Atmo Constants*********/

        own.ATMOS_TSTDABS = 518.67;        /* Standard Temperature - °R */
        own.ATMOS_T0 = 459.67;             /* Freezing Point       - °R */
        own.ATMOS_TEMPGRAD = -3.56616e-03; /* Temperature Gradient - °F/ft */
        own.ATMOS_PRESSEXPNT = -5.255876;  /* Pressure Exponent    - none */
        own.ATMOS_VV1 = 49.0223;           /* Sound Speed coefficient */
        own.ATMOS_A0 = 1.24871;            /* Vapor Pressure coefficients */
        own.ATMOS_A1 = 0.0988438;
        own.ATMOS_A2 = 0.00152907;
        own.ATMOS_A3 = -3.07031e-06;
        own.ATMOS_A4 = 4.21329e-07;
        own.ATMOS_ETCONV = 3.342e-04;
        own.ATMOS_TEMPSTD = 59.0;
        own.ATMOS_PRESSSTD = 29.92;
        own.ATMOS_HUMSTD = 0.0;
        own.ATMOS_ALTSTD = 0.0;
        own.ATMOS_MACHSTD = 1116.4499;
        own.ATMOS_DENSSTD = 0.076474;

        /************Fields**************/

        var temperature = 0;   /* temperature in °F */
        var pressure = 0;      /* pressure in in Hg    */
        var humidity = 0;      /* relative humdity     */
        var altitude = 0;      /* altitude in feet     */
        var mach = 0;          /* mach 1.0 in feet/sec */
        var density = 0;       /* atmospheric density  */

        /*********Fields Getters*******/
        own.getTemperature = function() {
            return temperature;
        };

        own.getPressure = function() {
            return pressure;
        };

        own.getHumidity = function() {
            return humidity;
        };

        own.getAltitude = function() {
            return altitude;
        };

        own.getMach = function() {
            return mach;
        };

        own.getDensity = function() {
            return density;
        };

       function calculate() {
           var t, p, hc, et, et0;
           t = temperature;
           p = pressure;

           if (t > 0.0) {
               et0 = own.ATMOS_A0 + t * (own.ATMOS_A1 + t * (own.ATMOS_A2 + t * (own.ATMOS_A3 + t * own.ATMOS_A4)));
               et = own.ATMOS_ETCONV * humidity * et0;
               hc = (p - 0.3783 * et) / own.ATMOS_PRESSSTD;
           } else {
               hc = 1.0;
           }
           density = own.ATMOS_DENSSTD * (own.ATMOS_TSTDABS / (t + own.ATMOS_T0)) * hc;
           mach = Math.sqrt(t + own.ATMOS_T0) * own.ATMOS_VV1;
       }

        /***************Initial Actions****************/
        //Empty constructor imitation
        if (!check(atmoArgs) || !(check(atmoArgs.temperature) || check(atmoArgs.pressure) || check(atmoArgs.humidity) || check(atmoArgs.altitude))) {
            temperature = own.ATMOS_TEMPSTD;
            pressure = own.ATMOS_PRESSSTD;
            humidity = own.ATMOS_HUMSTD;
            altitude = own.ATMOS_ALTSTD;
            calculate();
        } else if (atmoArgs instanceof b.atmoArguments &&
                   checkNumeric(atmoArgs.temperature) &&
                   checkNumeric(atmoArgs.pressure) &&
                   checkNumeric(atmoArgs.humidity) &&
                   checkNumeric(atmoArgs.altitude)) {
            temperature = atmoArgs.temperature;
            pressure = atmoArgs.pressure;
            humidity = atmoArgs.humidity;
            altitude = atmoArgs.altitude;
            calculate();
        } else if (atmoArgs instanceof b.atmoArguments && checkNumeric(atmoArgs.altitude)) {
            altitude = atmoArgs.altitude;
            temperature = own.ATMOS_TSTDABS + altitude * own.ATMOS_TEMPGRAD;
            pressure = own.ATMOS_PRESSSTD * Math.pow(own.ATMOS_TSTDABS / temperature, own.ATMOS_PRESSEXPNT);
            temperature = temperature - own.ATMOS_T0; /* line above need absolute! */
            humidity = own.ATMOS_HUMSTD;
            calculate();
        } else {
            throw "Incorrect arguments passed";
        }
    };

    /*******************Drag*****************/
    b.drag = function(dragTable) {
        var own = this;
        var parsedDragTable = parseUnit(dragTable, b.dragTable);
        if (!check(parsedDragTable)) throw "Passed drag table is incorrect";

        own.calculate = function(mach) {
            if (!checkNumeric(mach)) throw "Passed mach value is incorrect or undefined/empty";
            var calculationResult = 0;

            switch(parsedDragTable) {
                case b.dragTable.G1:
                    if (mach > 2.0) {
                        calculationResult = 0.9482590 + mach * (-0.248367 + mach * 0.0344343);
                    } else if (mach > 1.40) {
                        calculationResult = 0.6796810 + mach * (0.0705311 - mach * 0.0570628);
                    } else if (mach > 1.10) {
                        calculationResult = -1.471970 + mach * (3.1652900 - mach * 1.1728200);
                    } else if (mach > 0.85) {
                        calculationResult = -0.647392 + mach * (0.9421060 + mach * 0.1806040);
                    } else if (mach >= 0.55) {
                        calculationResult = 0.6224890 + mach * (-1.426820 + mach * 1.2094500);
                    } else {
                        calculationResult = 0.2637320 + mach * (-0.165665 + mach * 0.0852214);
                    }
                    break;
                case b.dragTable.G2:
                    if (mach > 2.5) {
                        calculationResult = 0.4465610 + mach * (-0.0958548 + mach * 0.00799645);
                    } else if (mach > 1.2) {
                        calculationResult = 0.7016110 + mach * (-0.3075100 + mach * 0.05192560);
                    } else if (mach > 1.0) {
                        calculationResult = -1.105010 + mach * (2.77195000 - mach * 1.26667000);
                    } else if (mach > 0.9) {
                        calculationResult = -2.240370 + mach * 2.63867000;
                    } else if (mach >= 0.7) {
                        calculationResult = 0.9099690 + mach * (-1.9017100 + mach * 1.21524000);
                    } else {
                        calculationResult = 0.2302760 + mach * (0.000210564 - mach * 0.1275050);
                    }
                    break;
                case b.dragTable.G5:
                    if (mach > 2.0) {
                        calculationResult = 0.671388 + mach * (-0.185208 + mach * 0.0204508);
                    } else if (mach > 1.1) {
                        calculationResult = 0.134374 + mach * (0.4378330 - mach * 0.1570190);
                    } else if (mach > 0.9) {
                        calculationResult = -0.924258 + mach * 1.24904;
                    } else if (mach >= 0.6) {
                        calculationResult = 0.654405 + mach * (-1.4275000 + mach * 0.998463);
                    } else {
                        calculationResult = 0.186386 + mach * (-0.0342136 - mach * 0.035691);
                    }
                    break;
                case b.dragTable.G6:
                    if (mach > 2.0) {
                        calculationResult = 0.746228 + mach * (-0.255926 + mach * 0.0291726);
                    } else if (mach > 1.1) {
                        calculationResult = 0.513638 + mach * (-0.015269 - mach * 0.0331221);
                    } else if (mach > 0.9) {
                        calculationResult = -0.908802 + mach * 1.25814;
                    } else if (mach >= 0.6) {
                        calculationResult = 0.366723 + mach * (-0.458435 + mach * 0.337906);
                    } else {
                        calculationResult = 0.264481 + mach * (-0.157237 + mach * 0.117441);
                    }
                    break;
                case b.dragTable.G7:
                    if (mach > 1.9) {
                        calculationResult = 0.439493 + mach * (-0.0793543 + mach * 0.00448477);
                    } else if (mach > 1.05) {
                        calculationResult = 0.642743 + mach * (-0.2725450 + mach * 0.049247500);
                    } else if (mach > 0.90) {
                        calculationResult = -1.69655 + mach * 2.03557;
                    } else if (mach >= 0.60) {
                        calculationResult = 0.353384 + mach * (-0.69240600 + mach * 0.50946900);
                    } else {
                        calculationResult = 0.119775 + mach * (-0.00231118 + mach * 0.00286712);
                    }
                    break;
                case b.dragTable.G8:
                    if (mach > 1.1) {
                        calculationResult = 0.639096 + mach * (-0.197471 + mach * 0.0216221);
                    } else if (mach >= 0.925) {
                        calculationResult = -12.9053 + mach * (24.9181 - mach * 11.6191);
                    } else {
                        calculationResult = 0.210589 + mach * (-0.00184895 + mach * 0.00211107);
                    }
                    break;
                case b.dragTable.GI:
                    if (mach > 1.0) {
                        calculationResult = 0.286629 + mach * (0.3588930 - mach * 0.0610598);
                    } else if (mach >= 0.8) {
                        calculationResult = 1.59969 + mach * (-3.9465500 + mach * 2.831370);
                    } else {
                        calculationResult = 0.333118 + mach * (-0.498448 + mach * 0.474774);
                    }
                    break;
                case b.dragTable.GL:
                    if (mach > 1.65) {
                        calculationResult = 0.845362 + mach * (-0.143989 + mach * 0.0113272);
                    } else if (mach > 1.2) {
                        calculationResult = 0.630556 + mach * 0.00701308;
                    } else if (mach >= 0.7) {
                        calculationResult = 0.531976 + mach * (-1.28079 + mach * 1.17628);
                    } else {
                        calculationResult = 0.2282;
                    }
                    break;
            }

            //if (isNaN(calculationResult)) throw "Calculation result is undefined or incorrect";
            return  calculationResult;
        }
    };


    /****************Ballistic Coefficient****************/
    b.ballisticCoefficient = function(bc, dragTable) {
        var own = this;
        /*************Ballistic Constants************/
        own.BC_PIR = 2.08551e-04;

        /*************Initial logic**************/
        if (!check(parseUnit(dragTable, b.dragTable))) throw "Passed drag table is incorrect";
        if (!checkNumeric(bc)) throw "Passed bc argument is incorrect or undefined/empty";

        own.getDrag = function(mach) {
            var drag = new b.drag(dragTable);
            var calculationResult = own.BC_PIR * drag.calculate(mach) / bc;
            if (!checkNumeric(calculationResult)) throw "Drag calculation result is incorrect or undefined/empty";
            return calculationResult;
        }
    };

    /***************Vector arguments***********/
    b.vector = function(x, y, z) {
        var own = this;
        if (!checkNumeric(x)) {
            own.x = 0;
        } else {
            own.x = x;
        }

        if (!checkNumeric(y)) {
            own.y = 0;
        } else {
            own.y = y;
        }

        if (!checkNumeric(z)) {
            own.z = 0;
        } else {
            own.z = z;
        }
    };

    /******************Vector*****************/
    function vectorLogic() {
        var own = this;

        own.dotX = function(vectorFirst, vectorSecond) {
            if (!check(vectorFirst) || !check(vectorSecond)) throw "dotX cannot be finished, cause passed incorrect arguments";
            checkVector(vectorFirst);
            checkVector(vectorSecond);
            return vectorFirst.x * vectorSecond.x;
        };

        /*
         *
         * */
        own.dotY = function(vectorFirst, vectorSecond) {
            if (!check(vectorFirst) || !check(vectorSecond)) throw "dotY cannot be finished, cause passed incorrect arguments";
            checkVector(vectorFirst);
            checkVector(vectorSecond);
            return vectorFirst.y * vectorSecond.y;
        };

        /*
         *
         * */
        own.dotZ = function(vectorFirst, vectorSecond) {
            if (!check(vectorFirst) || !check(vectorSecond)) throw "dotZ cannot be finished, cause passed incorrect arguments";
            checkVector(vectorFirst);
            checkVector(vectorSecond);
            return vectorFirst.z * vectorSecond.z;
        };

        /*
         *
         * */
        own.dot = function(vectorFirst, vectorSecond) {
            if (!check(vectorFirst) || !check(vectorSecond)) throw "dot cannot be finished, cause passed incorrect arguments";
            checkVector(vectorFirst);
            checkVector(vectorSecond);
            return own.dotX(vectorFirst, vectorSecond) + own.dotY(vectorFirst, vectorSecond) + own.dotZ(vectorFirst, vectorSecond);
        };

        /*
         *
         * */
        own.add = function(vectorFirst, vectorSecond) {
            if (!check(vectorFirst) || !check(vectorSecond)) throw "add cannot be finished, cause passed incorrect arguments";
            checkVector(vectorFirst);
            checkVector(vectorSecond);

            var newVector = new b.vector(vectorFirst.x + vectorSecond.x,
                                                 vectorFirst.y + vectorSecond.y,
                                                 vectorFirst.z + vectorSecond.z);
            return newVector;
        };

        /*
         *
         * */
        own.multiply = function(numericValue, vector) {
            if (!checkNumeric(numericValue) || !check(vector)) throw "multiply cannot be finished, cause passed incorrect arguments";
            checkVector(vector);

            var newVector = new b.vector(vector.x * numericValue,
                                                 vector.y * numericValue,
                                                 vector.z * numericValue);
            return newVector;
        };

        /*
         *
         * */
        own.subtract = function(vectorFirst, vectorSecond) {
            if (!check(vectorFirst) || !check(vectorSecond)) throw "subtract cannot be finished, cause passed incorrect arguments";
            checkVector(vectorFirst);
            checkVector(vectorSecond);

            var newVector = new b.vector(vectorFirst.x - vectorSecond.x,
                                                 vectorFirst.y - vectorSecond.y,
                                                 vectorFirst.z - vectorSecond.z);

            return newVector;
        };

        /*
         *
         * */
        own.reverse = function(vector) {
            if (!check(vector)) throw "reverse cannot be finished, cause passed incorrect arguments";
            checkVector(vector);

            var newVector = new b.vector((-1) * vector.x, (-1) * vector.y, (-1) * vector.z);

            return newVector;
        };

        /*
         *
         * */
        own.modulus = function(vector) {
            if (!check(vector)) throw "add cannot be finished, cause passed incorrect arguments";
            checkVector(vector);
            return own.dot(vector, vector);
        };

        /*
         *
         * */
        own.vectorLength = function(vector) {
            if (!check(vector)) throw "vectorLength cannot be finished, cause passed incorrect arguments";
            checkVector(vector);
            return Math.sqrt(own.modulus(vector));
        };

        /*
         *
         * */
        own.distance = function(vectorFirst, vectorSecond) {
            if (!check(vectorFirst) || !check(vectorSecond)) throw "distance cannot be finished, cause passed incorrect arguments";
            checkVector(vectorFirst);
            checkVector(vectorSecond);
            return own.vectorLength(own.subtract(vectorFirst, vectorSecond));
        };

        /*
         *
         * */
        own.normalize = function(vector) {
            if (!check(vector)) throw "add cannot be finished, cause passed incorrect arguments";
            checkVector(vector);
            var vectorLength = own.vectorLength(vector);
            if (vectorLength > 0) {
                return own.multiply(1.0 / vectorLength, vector);
            } else {
                return new b.vector();
            }
        }
    }


    /******************Trajectory*****************/

    b.bullet = function(bc, dragTable) {
        var own = this;
        var ballisticCoefficient = new b.ballisticCoefficient(bc, dragTable);
        own.getBallisticCoefficient = function() {
            return ballisticCoefficient;
        }
    };

    b.rifle = function(velocity, sightHeight) {
        var own = this;
        if (!checkNumeric(velocity) || !checkNumeric(sightHeight)) throw "Riffle object cannot be created, cause passed arguments are incorrect or undefined/empty";
        var ownVelocity = velocity;
        var ownSightHeight = sightHeight;

        /** Muzzle velocity in fps. */
        own.getVelocity = function() {
            return ownVelocity;
        };

        /** Muzzle velocity in feet. */
        own.getSightHeight = function() {
            return ownSightHeight;
        };
    };

    b.wind = function(velocity, direction) {
        var own = this;
        if (!checkNumeric(velocity) || !checkNumeric(direction)) throw "Wind object cannot be created, cause passed arguments are incorrect or undefined/empty";
        var ownVelocity = velocity;
        var ownDirection = direction;

        /** Wind velocity in fps. */
        own.getVelocity = function() {
            return ownVelocity;
        };

        /** Wind velocity in radians, 0 is straight from front. */
        own.getDirection = function() {
            return ownDirection;
        };
    };

    b.shot = function(los, cant, elevation) {
        var own = this;
        if (!checkNumeric(los) || !checkNumeric(cant) || !checkNumeric(elevation)) throw "Shot object cannot be created, cause passed arguments are incorrect or undefined/empty";
        var ownLineOfSightAngle = los;
        var ownCantAngle = cant;
        var ownElevation = elevation;

        /** Angle between line of sight and surface in radians. */
        own.getLineOfSightAngle = function() {
            return ownLineOfSightAngle;
        };

        /** Angle between the line connecting center of sight and center of bore and
         * the normal to the surface in radians. */
        own.getCantAngle = function() {
            return ownCantAngle;
        };

        /** Angle of the elevation of the barrel, in radians. */
        own.getElevation = function() {
            return ownElevation;
        };
    };

    b.rangeDataArguments = function() {
        var own = this;
        own.time = 0;
        own.range = 0;
        own.velocity = 0;
        own.mach = 0;
        own.drop = 0;
        own.windage = 0;
    };

    b.rangeData = function(rangeDataArgs) {
        var own = this;
        if (!check(rangeDataArgs) || (!checkNumeric(rangeDataArgs.time) ||
            !checkNumeric(rangeDataArgs.range) || !checkNumeric(rangeDataArgs.velocity) ||
            !checkNumeric(rangeDataArgs.mach) || !checkNumeric(rangeDataArgs.drop) ||
            !checkNumeric(rangeDataArgs.windage))) throw "Range data cannot be created, cause arguments are empty or undefined";
        if (!rangeDataArgs instanceof b.rangeDataArguments) throw "Passed arguments is not instance of Ballistic rangeDataArguments";

        var time = rangeDataArgs.time;
        var range = rangeDataArgs.range;
        var velocity = rangeDataArgs.velocity;
        var mach = rangeDataArgs.mach;
        var drop = rangeDataArgs.drop;
        var windage = rangeDataArgs.windage;

        /** Time in flight, in seconds. */
        own.getTime = function() {
            return time;
        };

        /** Range in feet. */
        own.getRange = function() {
            return range;
        };

        /** Velocity in ft/s. */
        own.getVelocity = function() {
            return velocity;
        };

        /** Velocity in ft/s. */
        own.getMach = function() {
            return mach;
        };

        /** Drop in ft. */
        own.getDrop = function() {
            return drop;
        };

        /** Windage in ft. */
        own.getWindage = function() {
            return windage;
        }

    };





    /***************Calculations************/
     function jbmCalculator() {
        var own = this;
        own.TRAJ_ERROR = 0.02 / 12.0;
        own.TRAJ_ABSMAXVEL = 5000.0;
        own.TRAJ_ABSMINVX = 50.0;
        own.TRAJ_ABSMINY = -199999.9 / 12.0;
        own.TRAJ_MAXRANGE = 2000;
        own.TRAJ_MAXITCNT = (10);
        own.TRAJ_GM = -32.17;
        own.TRAJ_GRAVITY = new b.vector(0, own.TRAJ_GM, 0);


        own.calculationArguments = function(){
            var ownArgs = this;
            ownArgs.bullet = null;
            ownArgs.rifle = null;
            ownArgs.atmo = null;
            ownArgs.shot = null;
            ownArgs.wind = null;
            ownArgs.zeroRange = null;
            ownArgs.nearZero = null;
            ownArgs.rangeToOrSize = null;
            ownArgs.step = null;
            ownArgs.target = null;
        };
        /** all ranges are in feet.

         * zeroRange, rangeTo and step are in feet.
         * calculateElevation is true in case zeroRange is specified and the barrel elevation
         * shall be calculated and saved to shot.elevation for the zero range specified.
         * If this flag is false, zeroRange is ignored and show.elevation
         * is used instead.
         *
         * calculateRanges is used in case RangeData[] array shall be generated.
         */
        own.calculate = function(calcArgs) {
            if (check(calcArgs) && !calcArgs instanceof own.calculationArguments)
                throw "Calculation arguments are undefined or not instance of Ballistic calculationArguments";
            if (check(calcArgs.bullet) && !calcArgs.bullet instanceof b.bullet)
                throw "Bullet object is incorrect or not instance of Ballistic bullet";
            if (check(calcArgs.rifle) && !calcArgs.rifle instanceof b.rifle)
                throw "Rifle object is incorrect or not instance of Ballistic rifle";
            if (check(calcArgs.atmo) && !calcArgs.atmo instanceof b.atmo)
                throw "Atmo object is incorrect or not instance of Ballistic atmo";
            if (check(calcArgs.shot) && !calcArgs.shot instanceof b.shot)
                throw "Shot object is incorrect or not instance of Ballistic shot";
            if (check(calcArgs.wind) && !calcArgs.wind instanceof b.wind)
                throw "Wind object is incorrect or not instance of Ballistic wind";
            if (!check(calcArgs.target)) throw "Calculation target is undefined or empty";

            var parsedCalculationTarget = parseUnit(calcArgs.target, b.trajectoryTarget);
            if (!check(parsedCalculationTarget))
                throw "Calculation target is incorrect";

            var calculateElevation = (parsedCalculationTarget === b.trajectoryTarget.DangerZone ||
                                      parsedCalculationTarget === b.trajectoryTarget.ElevationAngle ||
                                      parsedCalculationTarget === b.trajectoryTarget.ElevationAngleAndRange);

            var calculateRanges = (parsedCalculationTarget === b.trajectoryTarget.Range ||
                                   parsedCalculationTarget === b.trajectoryTarget.ElevationAngleAndRange);

            var calculateDangerZone = (parsedCalculationTarget === b.trajectoryTarget.DangerZone);

            var rangeTo = 0;
            var halfTarget = 0;

            if (parsedCalculationTarget === b.trajectoryTarget.DangerZone) {
                rangeTo = 3000;
                if (rangeTo < calcArgs.zeroRange) {
                    rangeTo = calcArgs.zeroRange * 2;
                }
                calcArgs.step = 1;
                halfTarget = calcArgs.rangeToOrSize / 2;
            } else {
                rangeTo = calcArgs.rangeToOrSize;
            }

            var TRAJ_DX = calcArgs.step / 10;

            while (TRAJ_DX > 1) {
                TRAJ_DX /= 10;
            }

            if (!check(calcArgs.atmo)) {
                calcArgs.atmo = new b.atmo();
            }


            if (!check(calcArgs.wind)) {
                calcArgs.wind = new b.wind(0, 0);
            }

            /*****************Variables***************/
            var dr = new b.vector();
            var r = new b.vector();
            var tv = new b.vector();
            var v = new b.vector();
            var w = new b.vector();
            var g = new b.vector();
            var z = new b.vector();
            var mv, vm, azim, elev;
            var dt, eq, t, mach, drg, err, dy, dz;
            var k, n;
            var j, itcnt;


            /** Expected number of range items. */
            j = Math.round(((rangeTo + 1) / calcArgs.step) + 1);
            if (!isFinite(j)) throw "Array length is infinite";
            var ranges = null;

            /** Position of the zero point. */
            z = new b.vector(calcArgs.zeroRange, 0, 0);


            if (calculateRanges) {
                ranges = new Array(j);
            } else if (calculateDangerZone) {
                ranges = new Array(2);
            }

            /** Mach velocity for the current atmosphere. */
            mach = calcArgs.atmo.getMach();

            /** Atmospeshpere factor for adjusting drag. */
            eq = calcArgs.atmo.getDensity() / calcArgs.atmo.ATMOS_DENSSTD;

            /* correct the gravity vector according */
            g = getGravity(calcArgs.shot);

            /* get wind components as a vector of velocities. */
            w = correctWind(calcArgs.shot, calcArgs.wind);

            /** muzzle velocity. */
            mv = calcArgs.rifle.getVelocity();

            /** Azimuth and elevation of the barrel vs the target. */
            azim = 0.0;
            if (calculateElevation) {
                elev = 0;
            } else {
                elev = calcArgs.shot.getElevation();
            }

            err = 0.0;      //error of zero finding
            itcnt = 0;      //number of iteration to find zero

            while (((err > own.TRAJ_ERROR) && (itcnt < own.TRAJ_MAXITCNT)) || (itcnt == 0)) {
                vm = mv;
                t = 0.0;

                //initial position: x - distance towards target, y - drop and z - widage. */
                r = new b.vector(0.0, (-1) * calcArgs.rifle.getSightHeight(), 0);
                //find out the components of velocity.
                v = b.vectorLogic.multiply(vm, new b.vector(Math.cos(elev) * Math.cos(azim), Math.sin(elev), Math.cos(elev) * Math.sin(azim)));

                j = 0;
                k = Math.max((calculateRanges || calculateDangerZone) ? rangeTo + 1 : z.x + 1, calculateElevation ? z.x + 1 : 0);
                n = 0;

                var inDangerZone = false;
                var hitDuringDangerZone = false;
                var dangerZoneFound = false;

                //run all the way down the range
                while (r.x <= k + TRAJ_DX) {
                    //if bullet too slow and fell too low - stop
                    if ((vm < own.TRAJ_ABSMINVX) || (r.y < own.TRAJ_ABSMINY)) {
                        break;
                    }

                    //if calculate the range records and the next range record is reached
                    if (calculateRanges && r.x >= n)
                    {
                        //save range
                        var rangeDataArgs = new b.rangeDataArguments();
                        rangeDataArgs.time = t;
                        rangeDataArgs.range = r.x;
                        rangeDataArgs.velocity = vm;
                        rangeDataArgs.mach = vm / mach;
                        rangeDataArgs.drop = r.y;
                        rangeDataArgs.windage = r.z;
                        var ballisticRange = new b.rangeData(rangeDataArgs);
                        ranges[j] = ballisticRange;
                        //and find out the next step
                        n += calcArgs.step;

                        j++;
                        if (j == ranges.length) {
                            break;
                        }
                    }

                    if (calculateDangerZone && !dangerZoneFound && r.y <= halfTarget && r.y >= -halfTarget) {
                        if (!inDangerZone) {
                            inDangerZone = true;

                            var rangeDataArgs = new b.rangeDataArguments();
                            rangeDataArgs.time = t;
                            rangeDataArgs.range = r.x;
                            rangeDataArgs.velocity = vm;
                            rangeDataArgs.mach = vm / mach;
                            rangeDataArgs.drop = r.y;
                            rangeDataArgs.windage = r.z;
                            var ballisticRange = new b.rangeData(rangeDataArgs);
                            ranges[0] = ballisticRange;
                        } else {
                            if (Math.abs(r.x - z.x) < 0.5 * TRAJ_DX) {
                                hitDuringDangerZone = true;
                            }
                        }
                    } else {
                        if (inDangerZone) {
                            if (hitDuringDangerZone) {
                                var rangeDataArgs = new b.rangeDataArguments();
                                rangeDataArgs.time = t;
                                rangeDataArgs.range = r.x;
                                rangeDataArgs.velocity = vm;
                                rangeDataArgs.mach = vm / mach;
                                rangeDataArgs.drop = r.y;
                                rangeDataArgs.windage = r.z;
                                var ballisticRange = new b.rangeData(rangeDataArgs);
                                ranges[1] = ballisticRange;
                                dangerZoneFound = true;
                            }
                            inDangerZone = false;
                        }
                    }

                    //calculate velocity change for 1/2 of a step.
                    dt = 0.5 * TRAJ_DX / v.x;
                    tv = b.vectorLogic.subtract(v, w);                                     //adjust wind
                    vm = b.vectorLogic.vectorLength(tv);
                    drg = eq * vm * calcArgs.bullet.getBallisticCoefficient().getDrag(vm / mach); //find drag (change of velocity)
                    tv = b.vectorLogic.subtract(v, b.vectorLogic.multiply(dt, b.vectorLogic.subtract(b.vectorLogic.multiply(drg, tv), g))); //find new velocity

                    //calculate velocity change for another 1/2 of a step.
                    dt = TRAJ_DX / tv.x;
                    tv = b.vectorLogic.subtract(tv, w);
                    vm = b.vectorLogic.vectorLength(tv);
                    drg = eq * vm * calcArgs.bullet.getBallisticCoefficient().getDrag(vm / mach);
                    v = b.vectorLogic.subtract(v, b.vectorLogic.multiply(dt, b.vectorLogic.subtract(b.vectorLogic.multiply(drg, tv), g)));

                    //calculate new position
                    dr = new b.vector(TRAJ_DX, v.y * dt, v.z * dt);
                    r = b.vectorLogic.add(r, dr);
                    vm = b.vectorLogic.vectorLength(v);
                    //and time in fly
                    t = t + b.vectorLogic.vectorLength(dr) / vm;

                    //if elevation is being calculate and we are at zero range
                    if (calculateElevation && (Math.abs(r.x - z.x) < 0.5 * TRAJ_DX)) {
                        //find error and adjust the elevation angle for the next iteration.
                        dy = r.y - z.y;
                        dz = r.z - z.z;
                        err = Math.abs(dy);
                        elev = elev - dy / r.x;
                        //no reason to finish calculation if error is too big, go to
                        //next iteration immeditelly.
                        if (err > own.TRAJ_ERROR) break;
                    }

                }
                if (inDangerZone && hitDuringDangerZone) {
                    var rangeDataArgs = new b.rangeDataArguments();
                    rangeDataArgs.time = t;
                    rangeDataArgs.range = r.x;
                    rangeDataArgs.velocity = vm;
                    rangeDataArgs.mach = vm / mach;
                    rangeDataArgs.drop = r.y;
                    rangeDataArgs.windage = r.z;
                    var ballisticRange = new b.rangeData(rangeDataArgs);
                    ranges[1] = ballisticRange;
                }
                itcnt++;
            }

            var resultMap = {};

            if (calculateElevation) {
                resultMap.elevationAngle = elev;
            } else {
                resultMap.elevationAngle = calcArgs.shot.getElevation();
            }

            resultMap.ranges = ranges;
            return resultMap;
        };

        function getGravity(shot) {
            if (!check(shot) || !shot instanceof b.shot) throw "Show object is undefined or not instance of Ballistic shot";
            var cl = Math.cos(shot.getLineOfSightAngle());
            var sl = Math.sin(shot.getLineOfSightAngle());
            var cc = Math.cos(shot.getCantAngle());
            var sc = Math.sin(shot.getCantAngle());

            return new b.vector(own.TRAJ_GM * sl,  own.TRAJ_GM * cl * cc, (-own.TRAJ_GM) * cl * sc)
        }

        function correctWind(shot, wind) {

            if (!check(shot) || !shot instanceof b.shot) throw "Show object is undefined or not instance of Ballistic shot";
            if (!check(wind) || !wind instanceof b.wind) throw "Wind object is undefined or not instance of Ballistic wind";

            var cl = Math.cos(shot.getLineOfSightAngle());;
            var sl = Math.sin(shot.getLineOfSightAngle());
            var cc = Math.cos(shot.getCantAngle());
            var sc = Math.sin(shot.getCantAngle());

            /* range speed. */
            var wx = wind.getVelocity() * Math.cos(wind.getDirection());
            /* cross speed. */
            var wz = wind.getVelocity() * Math.sin(wind.getDirection());
            var w = new b.vector(wx, 0, wz);

            var tmp = w.y * cl - w.x * sl;
            return new b.vector(w.x * cl + w.y * sl, tmp * cc + w.z * sc, w.z * cc - tmp * sc);
        }
    }


    /******************Info Logic*************/

    /***************Ammo Info***************/
    b.ammoInfoArguments = function() {
        var own = this;
        own.dragTable = null;
        own.ballisticCoefficient = null;
        own.muzzleVelocity = null;
        own.bulletWeight = null;
    };

    b.ammoInfo = function(ammoInfoArgs) {
        var own = this;

        var dragTable = b.dragTable.G1;
        var ballisticCoefficient = 0.5;
        var muzzleVelocity = new b.velocity(3000, b.velocityUnits.FeetPerSecond);
        var bulletWeight = new b.weight(150, b.weightUnits.Grain);

        own.setDragTable = function(dragTableArg) {
            if (!check(dragTableArg) || !dragTableArg instanceof b.dragTable)
                throw "Ammo dragTableArg argument is incorrect";
            dragTable = dragTableArg;
        };

        own.getDragTable = function() {
            return dragTable;
        };


        own.setBallisticCoefficient = function(ballisticCoefficientArg) {
            if (!checkNumeric(ballisticCoefficientArg))
                throw "Ammo ballisticCoefficient argument is incorrect";
            ballisticCoefficient = ballisticCoefficientArg;
        };

        own.getBallisticCoefficient = function() {
            return ballisticCoefficient;
        };

        own.setMuzzleVelocity = function(muzzleVelocityArg) {
            if (!check(muzzleVelocityArg) || !muzzleVelocityArg instanceof b.velocity)
                throw "Ammo muzzleVelocityArg argument is incorrect";
            muzzleVelocity = muzzleVelocityArg;
        };

        own.getMuzzleVelocity = function() {
            return muzzleVelocity;
        };

        own.setBulletWeight = function(bulletWeightArg) {
            if (!check(bulletWeightArg) || !bulletWeightArg instanceof b.weight)
                throw "Ammo bulletWeightArg argument is incorrect";
            bulletWeight = bulletWeightArg;
        };

        own.getBulletWeight = function() {
            return bulletWeight;
        };


        var isValidArgs = check(ammoInfoArgs) && ammoInfoArgs instanceof  b.ammoInfoArguments;

        if (check(ammoInfoArgs) && !ammoInfoArgs instanceof  b.ammoInfoArguments)
            throw "Ammo info arguments is not instance of Ballistic ammo info arguments";

        if (isValidArgs) {
            if (!check(ammoInfoArgs.dragTable))
                throw "Ammo dragTable argument is incorrect";
            if (!checkNumeric(ammoInfoArgs.ballisticCoefficient))
                throw "Ammo ballisticCoefficient argument is incorrect";
            if (!check(ammoInfoArgs.muzzleVelocity) || !ammoInfoArgs.muzzleVelocity instanceof b.velocity)
                throw "Ammo muzzleVelocity argument is incorrect";

            if (!check(ammoInfoArgs.bulletWeight) || !ammoInfoArgs.bulletWeight instanceof b.weight)
                throw "Ammo bulletWeight argument is incorrect";

            dragTable = ammoInfoArgs.dragTable;
            ballisticCoefficient = ammoInfoArgs.ballisticCoefficient;
            muzzleVelocity = ammoInfoArgs.muzzleVelocity;
            bulletWeight = ammoInfoArgs.bulletWeight;
        }

    };


    /****************Atmosphere****************/

    b.atmosphereInfoArguments = function() {
        var own = this;
        own.altitude = null;
        own.pressure = null;
        own.temperature = null;
        own.humidity = null;
    };

    b.atmosphereInfo = function(atmosphereArgs) {
        var own = this;

        var altitude = new b.distance(0, b.distanceUnits.Foot);
        var pressure = new b.pressure(29.53, b.pressureUnits.InchHg);
        var temperature = new b.temperature(59, b.temperatureUnits.Fahrenheit);
        var humidity = 0.78;

        own.setAltitude = function(altitudeArg) {
            if (!check(altitudeArg))
                throw "Atmosphere altitude argument is incorrect";
            altitude = altitudeArg;
        };

        own.getAltitude = function() {
            return altitude;
        };

        own.setHumidity = function(humidityArg) {
            if (!checkNumeric(humidityArg))
                throw "Atmosphere humidity argument is incorrect";
            humidity = humidityArg;
        };

        own.getHumidity = function() {
            return humidity;
        };

        own.setPressure = function(pressureArg) {
            if (!check(pressureArg) || !pressureArg instanceof b.pressure)
                throw "Atmosphere pressure argument is incorrect";
            pressure = pressureArg;
        };

        own.getPressure = function() {
            return pressure;
        };

        own.setTemperature = function(temperatureArg) {
            if (!check(temperatureArg) || !temperatureArg instanceof b.temperature)
                throw "Atmosphere temperature argument is incorrect";
            temperature = temperatureArg;
        };

        own.getTemperature = function() {
            return temperature;
        };

        var isValidArgs = check(atmosphereArgs) && atmosphereArgs instanceof  b.atmosphereInfoArguments;

        if (check(atmosphereArgs) && !atmosphereArgs instanceof  b.atmosphereInfoArguments)
            throw "Atmosphere arguments is not instance of Ballistic atmosphere  arguments";

        if (isValidArgs) {
            if (!check(atmosphereArgs.altitude))
                throw "Atmosphere altitude argument is incorrect";
            if (!checkNumeric(atmosphereArgs.humidity))
                throw "Atmosphere humidity argument is incorrect";
            if (!check(atmosphereArgs.pressure) || !atmosphereArgs.pressure instanceof b.pressure)
                throw "Atmosphere pressure argument is incorrect";

            if (!check(atmosphereArgs.temperature) || !atmosphereArgs.temperature instanceof b.temperature)
                throw "Atmosphere temperature argument is incorrect";

            altitude = atmosphereArgs.altitude;
            pressure = atmosphereArgs.pressure;
            temperature = atmosphereArgs.temperature;
            humidity = atmosphereArgs.humidity;
        }

    };

    /*******************Drift Info****************/

    function driftInfoController() {
        var own = this;

        //Unhandled exceptions
        own.calculateStabilityCoefficient = function(ammoInfo, driftInfo, atmosphere) {

            var w = ammoInfo.getBulletWeight().get(b.weightUnits.Grain);
            var t = driftInfo.getRiflingTwist().get(b.distanceUnits.Inch) / driftInfo.getBulletDiameter().get(b.distanceUnits.Inch);
            var d = driftInfo.getBulletDiameter().get(b.distanceUnits.Inch);
            var l = driftInfo.getBulletLength().get(b.distanceUnits.Inch) / driftInfo.getBulletDiameter().get(b.distanceUnits.Inch);

            var sd = 30 * w / (Math.pow(t, 2) * Math.pow(d, 3) * l * (1 + Math.pow(l, 2)));
            //correct for muzzle velocity
            var fv = Math.pow(ammoInfo.getMuzzleVelocity().get(b.velocityUnits.FeetPerSecond) / 2800, 1.0 / 3.0);
            var ftp = 1;

            if (check(atmosphere)) {
                var ft = atmosphere.getTemperature().get(b.temperatureUnits.Fahrenheit);
                var pt = atmosphere.getPressure().get(b.pressureUnits.InchHg);
                ftp = ((ft + 460) / (59 + 460)) * (29.92 / pt);
            }

            return sd * fv * ftp;
        };

        //Unhandled exceptions
        own.calculateDrift = function(driftInfo, stabilityCoefficient, timeOfFly) {
            return new b.distance(1.25 * (stabilityCoefficient + 1.2) * Math.pow(timeOfFly, 1.83) * (driftInfo.isRightHandTwist() ? -1 : 1) , b.distanceUnits.Inch);
        }
    }

    b.driftInfoArguments = function() {
        var own = this;
        own.bulletLength = null;
        own.bulletDiameter = null;
        own.riflingTwist = null;
        own.rightHandTwist = null;
    };

    b.driftInfo = function(driftInfoArgs) {
        var own = this;
        var bulletLength;
        var bulletDiameter;
        var riflingTwist;
        var rightHandTwist;

        own.setBulletLength = function(bulletLengthArg) {
            if (!check(bulletLengthArg) || !bulletLengthArg instanceof b.distance)
                throw "Drift info bullet length argument is incorrect";
            bulletLength = bulletLengthArg;
        };

        own.getBulletLength = function() {
            return bulletLength;
        };

        own.setBulletDiameter = function(bulletDiameterArg) {
            if (!check(bulletDiameterArg) || !bulletDiameterArg instanceof b.distance)
                throw "Drift info bullet diameter argument is incorrect";
            bulletDiameter = bulletDiameterArg;
        };

        own.getBulletDiameter = function() {
            return bulletDiameter;
        };

        own.setRiflingTwist = function(riflingTwistArg) {
            if (!check(riflingTwistArg) || !riflingTwistArg instanceof b.distance)
                throw "Drift info rifling twist argument is incorrect";
            riflingTwist = riflingTwistArg;
        };

        own.getRiflingTwist = function() {
            return riflingTwist;
        };

        own.setRightHandTwist = function(rightHandTwistArg) {
            if (!checkBool(rightHandTwistArg))
                throw "Drift info right hand twist argument is incorrect";
            rightHandTwist = rightHandTwistArg;
        };

        own.getRightHandTwist = function() {
            return rightHandTwist;
        };

        var isValidArgs = check(driftInfoArgs) && driftInfoArgs instanceof  b.driftInfoArguments;

        if (check(driftInfoArgs) && !driftInfoArgs instanceof  b.driftInfoArguments)
            throw "Drift info arguments is not instance of Ballistic drift info arguments";

        if (isValidArgs) {
            if (!check(driftInfoArgs.bulletLength) || !driftInfoArgs.bulletLength instanceof b.distance)
                throw "Drift info bullet length argument is incorrect";
            if (!check(driftInfoArgs.bulletDiameter) || !driftInfoArgs.bulletDiameter instanceof b.distance)
                throw "Drift info bullet diameter argument is incorrect";
            if (!check(driftInfoArgs.riflingTwist) || !driftInfoArgs.riflingTwist instanceof b.distance)
                throw "Drift info rifling twist argument is incorrect";
            if (!checkBool(driftInfoArgs.rightHandTwist))
                throw "Drift info right hand twist argument is incorrect";

            bulletLength = driftInfoArgs.bulletLength;
            bulletDiameter = driftInfoArgs.bulletDiameter;
            riflingTwist = driftInfoArgs.riflingTwist;
            rightHandTwist = driftInfoArgs.rightHandTwist;
        }

    };

    /***************Wind Info**************/
    b.windInfoArguments = function() {
        var own = this;
        own.direction = null;
        own.speed = null;
    };

    b.windInfo = function(windInfoArgs) {
        var own = this;

        var direction = null;
        var speed = null;

        own.setDirection = function(directionArg) {
            if (!check(directionArg) || !directionArg instanceof b.angle)
                throw "Wind info direction argument is incorrect";
            direction = directionArg;
        };

        own.getDirection = function() {
            return direction;
        };

        own.setSpeed = function(speedArg) {
            if (!check(speedArg) || !speedArg instanceof b.velocity)
                throw "Wind info speed argument is incorrect";
            speed = speedArg;
        };

        own.getSpeed = function() {
            return speed;
        };

        own.isComplete = function() {
            return !check(direction) && !check(speed);
        };

        var isValidArgs = check(windInfoArgs) && windInfoArgs instanceof b.windInfoArguments;

        if (check(windInfoArgs) && !windInfoArgs instanceof b.windInfoArguments)
            throw "Wind info arguments is not instance of Ballistic wind info arguments";

        if (isValidArgs) {
            if (!check(windInfoArgs.direction) || !windInfoArgs.direction instanceof b.angle)
                throw "Wind info direction argument is incorrect";
            if (!check(windInfoArgs.speed) || !windInfoArgs.speed instanceof b.velocity)
                throw "Wind info speed argument is incorrect";
            direction = windInfoArgs.direction;
            speed = windInfoArgs.speed;
        }

    };

    /********************Shot Info*****************/
    b.shotInfoArguments = function() {
        var own = this;
        own.name = null;
        own.ammo = null;
        own.atmosphere = null;
        own.wind = null;
        own.sightHeight = null;
        own.zeroDistance = null;
        own.nearZero = null;
        own.shotAngle = null;
        own.cantAngle = null;
        own.elevationAngle = null;
        own.step = null;
        own.driftInfo = null;
        own.verticalClick = null;
        own.horizontalClick = null;
        own.targetSize = null;
        own.clicks = null;
        own.maxDistance = null;
    };

    b.shotInfo = function(shotInfoArgs) {
        var own = this;
        var name = null;
        var ammo = null;
        var atmosphere = null;
        var wind = null;
        var sightHeight = null;
        var zeroDistance = null;
        var nearZero = null;
        var shotAngle = null;
        var cantAngle = null;
        var elevationAngle = null;
        var maxDistance = null;
        var step = null;
        var driftInfo = null;
        var verticalClick = null;
        var horizontalClick = null;
        var targetSize = null;
        var clicks = null;


        own.setName = function(nameArg) {
            if (!check(name)) throw "Passed name is undefined";
            name = nameArg;
        };

        own.getName = function() {
           return name;
        };

        own.setAmmo = function(ammoArg) {
            if (!check(ammoArg) || !ammoArg instanceof b.ammoInfo)
                throw "Shot info ammo argument is incorrect";
            ammo = ammoArg;
        };

        own.getAmmo = function() {
            return ammo;
        };


        own.setAtmosphere = function(atmoArg) {
            if (!check(atmoArg) || !atmoArg instanceof b.atmosphereInfo)
                throw "Shot info atmo argument is incorrect";
            atmosphere = atmoArg;
        };

        own.getAtmosphere = function() {
            return atmosphere;
        };

        own.setWind = function(windArg) {
            if (!check(windArg) || !windArg instanceof b.windInfo)
                throw "Shot info wind argument is incorrect";
            wind = windArg;
        };

        own.getWind = function() {
            return wind;
        };

        own.setSightHeight = function(sightHeightArg) {
            if (!check(sightHeightArg) || !sightHeightArg instanceof b.distance)
                throw "Shot info sight height argument is incorrect";
            sightHeight = sightHeightArg;
        };

        own.getSightHeight = function() {
            return sightHeight;
        };


        own.setZeroDistance = function(zeroDistanceArg) {
            if (!check(zeroDistanceArg) || !zeroDistanceArg instanceof b.distance)
                throw "Shot info zero distance argument is incorrect";
            zeroDistance = zeroDistanceArg;
        };

        own.getZeroDistance = function() {
            return zeroDistance;
        };

        own.setNearZero = function(nearZeroArg) {
            if (!checkBool(nearZeroArg)) throw "Was passed incorrect near zero argument";
        };

        own.getNearZero = function() {
            return nearZero;
        };

        own.setShotAngle = function(shotAngleArg) {
            if (!check(shotAngleArg) || !shotAngleArg instanceof b.angle)
                throw "Shot info shot angle argument is incorrect";
            shotAngle = shotAngleArg;
        };

        own.getShotAngle = function() {
            return shotAngle;
        };


        own.setCantAngle = function(cantAngleArg) {
            if (!check(cantAngleArg) || !cantAngleArg instanceof b.angle)
                throw "Shot info cant angle argument is incorrect";
            cantAngle = cantAngleArg;
        };

        own.getCantAngle = function() {
            return cantAngle;
        };

        own.setElevationAngle = function(elevationAngleArg) {
            if (!check(elevationAngleArg) || !elevationAngleArg instanceof b.angle)
                throw "Shot info elevation angle argument is incorrect";
            elevationAngle = elevationAngleArg;
        };

        own.getElevationAngle = function() {
            return elevationAngle;
        };

        own.setMaxDistance = function(maxDistanceArg) {
            if (!check(maxDistanceArg) || !maxDistanceArg instanceof b.distance)
                throw "Shot info max distance argument is incorrect";
            maxDistance = maxDistanceArg;
        };

        own.getMaxDistance = function() {
            return maxDistance;
        };

        own.setStep = function(stepArg) {
            if (!check(stepArg) || !stepArg instanceof b.distance)
                throw "Shot info max distance argument is incorrect";
            step = stepArg;
        };

        own.getStep = function() {
            return step;
        };

        own.setDriftInfo = function(driftInfoArg) {
            if (!check(driftInfoArg) || !driftInfoArg instanceof b.driftInfo) throw "Shot info drift info argument is incorrect";
            driftInfo = driftInfoArg;
        };

        own.getDriftInfo = function() {
            return driftInfo;
        };

        own.setVerticalClick = function(verticalClickArg) {
            if (!check(verticalClickArg) || !verticalClickArg instanceof b.angle)
                throw "Shot info vertical click argument is incorrect";
            verticalClick = verticalClickArg;
        };

        own.getVerticalClick = function() {
            return verticalClick;
        };

        own.setHorizontalClick = function(horizontalClickArg) {
            if (!check(horizontalClickArg) || !horizontalClickArg instanceof b.angle)
                throw "Shot info horizontal click argument is incorrect";
            horizontalClick = horizontalClickArg;
        };

        own.getHorizontalClick = function() {
            return horizontalClick;
        };

        own.setTargetSize = function(targetSizeArg) {
            if (!check(targetSizeArg) || !targetSizeArg instanceof b.distance)
                throw "Shot info target size argument is incorrect";
            targetSize = targetSizeArg;
        };

        own.getTargetSize = function() {
            return targetSize;
        };

        own.setClicks = function(clicksArg) {
           if (!checkNumeric(clicksArg)) throw "Shot info click argument is incorrect";
            clicks = clicksArg;
        };

        own.getClicks = function() {
            return clicks;
        };

        var isValidArgs = check(shotInfoArgs) && shotInfoArgs instanceof  b.shotInfoArguments;

        if (check(shotInfoArgs) && !shotInfoArgs instanceof  b.shotInfoArguments)
            throw "Shot info arguments is not instance of Ballistic shot info arguments";

        if (isValidArgs) {
             name = shotInfoArgs.name;
             ammo = shotInfoArgs.ammo;
             elevationAngle = shotInfoArgs.elevationAngle;
             wind = shotInfoArgs.wind;
             atmosphere = shotInfoArgs.atmosphere;
             shotAngle = shotInfoArgs.shotAngle;
             cantAngle = shotInfoArgs.cantAngle;
             driftInfo = shotInfoArgs.driftInfo;
             wind = shotInfoArgs.wind;
             sightHeight = shotInfoArgs.sightHeight;
             zeroDistance = shotInfoArgs.zeroDistance;
             nearZero = shotInfoArgs.nearZero;
             verticalClick = shotInfoArgs.verticalClick;
             horizontalClick = shotInfoArgs.horizontalClick;
             targetSize = shotInfoArgs.targetSize;
             clicks = shotInfoArgs.clicks;
        }

        maxDistance = new b.distance(5000, b.distanceUnits.Yard);
        step = new b.distance(10, b.distanceUnits.Yard);
    };

    function shotInfoController() {
        var own = this;

        own.canCalculate = function(shot) {
            return check(shot) && check(shot.getAmmo()) && check(shot.getSightHeight());
        };

        own.calculateZero = function(zeroInfo) {
            var resultMap = {};
            resultMap.elevationAngle = null;
            resultMap.isProcessed = false;

            if (!check(zeroInfo) || !zeroInfo instanceof b.shotInfo) throw "Passed incorrect argument";

            if (!own.canCalculate(zeroInfo)) {
                return resultMap;
            }

            var atm = null;

            if (check(zeroInfo.getAtmosphere())) {
                var atmoArgs = new b.atmoArguments();
                atmoArgs.temperature = zeroInfo.getAtmosphere().getTemperature().get(b.temperatureUnits.Fahrenheit);
                atmoArgs.pressure = zeroInfo.getAtmosphere().getPressure().get(b.pressureUnits.InchHg);
                atmoArgs.humidity = zeroInfo.getAtmosphere().getHumidity();
                atmoArgs.altitude = zeroInfo.getAtmosphere().getAltitude().get(b.distanceUnits.Foot);
                atm = new b.atmo(atmoArgs);
            } else {
                atm = new b.atmo(undefined);
            }

            var bullet = new b.bullet(zeroInfo.getAmmo().getBallisticCoefficient(), zeroInfo.getAmmo().getDragTable());
            var rifle = new b.rifle(zeroInfo.getAmmo().getMuzzleVelocity().get(b.velocityUnits.FeetPerSecond), zeroInfo.getSightHeight().get(b.distanceUnits.Foot));

            var wind = null;
            if (check(zeroInfo.getWind())) {
                wind = new b.wind(zeroInfo.getWind().getSpeed().get(b.velocityUnits.FeetPerSecond), zeroInfo.getWind().getDirection().get(b.angleUnits.Radian));
            }

            var shot = new b.shot(zeroInfo.getShotAngle() != null ? zeroInfo.getShotAngle().get(b.angleUnits.Radian) : 0, check(zeroInfo.getCantAngle()) ? zeroInfo.getCantAngle().get(b.angleUnits.Radian) : 0, 0);

            var zero = zeroInfo.getZeroDistance().get(b.distanceUnits.Foot);

            var jbmArguments = new b.jbmCalculator.calculationArguments();
            jbmArguments.bullet = bullet;
            jbmArguments.rifle = rifle;
            jbmArguments.atmo = atm;
            jbmArguments.shot = shot;
            jbmArguments.wind = wind;
            jbmArguments.zeroRange = zero;
            jbmArguments.nearZero = zeroInfo.getNearZero();
            jbmArguments.rangeToOrSize = 0;
            jbmArguments.step = 1;
            jbmArguments.target = b.trajectoryTarget.ElevationAngle;
            var result = b.jbmCalculator.calculate(jbmArguments);
            if (check(result) && check(result.elevationAngle)) {
                resultMap.isProcessed = true;
                resultMap.elevationAngle = new b.angle(result.elevationAngle, b.angleUnits.Radian);
            }
            return resultMap;
        };

        own.calculateShot = function(shotInfo) {
            if (!check(shotInfo) || !shotInfo instanceof b.shotInfo) throw "Passed incorrect argument";

            if (!own.canCalculate(shotInfo)) {
                return null;
            }

            var atm = null;

            if (check(shotInfo.getAtmosphere())) {
                var atmoArgs = new b.atmoArguments();
                atmoArgs.temperature = shotInfo.getAtmosphere().getTemperature().get(b.temperatureUnits.Fahrenheit);
                atmoArgs.pressure = shotInfo.getAtmosphere().getPressure().get(b.pressureUnits.InchHg);
                atmoArgs.humidity = shotInfo.getAtmosphere().getHumidity();
                atmoArgs.altitude = shotInfo.getAtmosphere().getAltitude().get(b.distanceUnits.Foot);
                atm = new b.atmo(atmoArgs);
            } else {
                atm = new b.atmo(undefined);
            }

            var bullet = new b.bullet(shotInfo.getAmmo().getBallisticCoefficient(), shotInfo.getAmmo().getDragTable());
            var rifle = new b.rifle(shotInfo.getAmmo().getMuzzleVelocity().get(b.velocityUnits.FeetPerSecond), shotInfo.getSightHeight().get(b.distanceUnits.Foot));

            var wind = null;
            if (check(shotInfo.getWind())) {
                wind = new b.wind(shotInfo.getWind().getSpeed().get(b.velocityUnits.FeetPerSecond), shotInfo.getWind().getDirection().get(b.angleUnits.Radian));
            }

            var elevationAngle = check(shotInfo.getElevationAngle()) ? shotInfo.getElevationAngle().get(b.angleUnits.Radian) : 0;
            if (shotInfo.getClicks() != 0 && check(shotInfo.getVerticalClick())) {
                elevationAngle += shotInfo.getClicks() * shotInfo.getVerticalClick().get(b.angleUnits.Radian);
            }

            var shot = new b.shot(check(shotInfo.getShotAngle()) ? shotInfo.getShotAngle().get(b.angleUnits.Radian) : 0,
                                  check(shotInfo.getCantAngle()) ? shotInfo.getCantAngle().get(b.angleUnits.Radian) : 0, elevationAngle);

            var jbmArguments = new b.jbmCalculator.calculationArguments();
            jbmArguments.bullet = bullet;
            jbmArguments.rifle = rifle;
            jbmArguments.atmo = atm;
            jbmArguments.shot = shot;
            jbmArguments.wind = wind;
            jbmArguments.zeroRange = 0;
            jbmArguments.nearZero = false;
            jbmArguments.rangeToOrSize = shotInfo.getMaxDistance().get(b.distanceUnits.Foot);
            jbmArguments.step = shotInfo.getStep().get(b.distanceUnits.Foot);
            jbmArguments.target = b.trajectoryTarget.Range;

            var ballistic = b.jbmCalculator.calculate(jbmArguments);

            if (!check(ballistic) || !check(ballistic.ranges))
                return null;

            var ballisticCollection = new b.ballisticCollection();

            var bulletWeightGr = shotInfo.getAmmo().getBulletWeight().get(b.weightUnits.Grain);
            var stablityCoefficient = 0;
            if (check(shotInfo.getDriftInfo()))
                stablityCoefficient = b.driftInfoController.calculateStabilityCoefficient(shotInfo.getAmmo(), shotInfo.getDriftInfo(), shotInfo.getAtmosphere());

            for (var i = 0; i < ballistic.ranges.length; i++) {
                if (check(ballistic.ranges[i])) {
                    var v = ballistic.ranges[i].getVelocity();
                    var wi = ballistic.ranges[i].getWindage();
                    var si = 0;
                    if (shotInfo.getDriftInfo() != null)
                        si = b.driftInfoController.calculateDrift(shotInfo.getDriftInfo(), stablityCoefficient, ballistic.ranges[i].getTime()).get(b.distanceUnits.Foot);
                    wi += si;

                    var range = new b.distance(ballistic.ranges[i].getRange(), b.distanceUnits.Foot);
                    var path = new b.distance(ballistic.ranges[i].getDrop(), b.distanceUnits.Foot);
                    var hold = new b.angle(Math.atan(ballistic.ranges[i].getDrop() / ballistic.ranges[i].getRange()), b.angleUnits.Radian);
                    var velocity = new b.velocity(v, b.velocityUnits.FeetPerSecond);
                    var windage = new b.distance(wi, b.distanceUnits.Foot);
                    var windageCorrection = new b.angle(Math.atan(wi / ballistic.ranges[i].getRange()), b.angleUnits.Radian);
                    var ogv = new b.weight(Math.pow(v, 3) * Math.pow(bulletWeightGr, 2) * (1.5e-12), b.weightUnits.Pound);
                    var energy = new b.energy(bulletWeightGr * v * v / 450400, b.energyUnits.Joule);
                    var holdClicks = 0, windageClicks = 0;
                    if (check(shotInfo.getHorizontalClick()) && check(shotInfo.getHorizontalClick().get(b.angleUnits.Mil)))
                        windageClicks = (1) * Math.round(windageCorrection.get(b.angleUnits.Mil) / shotInfo.getHorizontalClick().get(b.angleUnits.Mil));
                    if (check(shotInfo.getVerticalClick()) && check(shotInfo.getVerticalClick().get(b.angleUnits.Mil)))
                        holdClicks = (1) * Math.round(hold.get(b.angleUnits.Mil) / shotInfo.getVerticalClick().get(b.angleUnits.Mil));


                    var bInfoArgs = new b.ballisticInfoArguments();
                    bInfoArgs.path = path;
                    bInfoArgs.range = range;
                    bInfoArgs.hold = hold;
                    bInfoArgs.time = ballistic.ranges[i].getTime();
                    bInfoArgs.windage = windage;
                    bInfoArgs.windageCorrection = windageCorrection;
                    bInfoArgs.bulletVelocity = velocity;
                    bInfoArgs.mach = ballistic.ranges[i].getMach();
                    bInfoArgs.bulletEnergy = energy;
                    bInfoArgs.optimalGameWeight = ogv;
                    bInfoArgs.holdClicks = holdClicks;
                    bInfoArgs.windageClicks = windageClicks;


                    var ballisticInfo = new b.ballisticInfo(bInfoArgs);
                    ballisticCollection.add(ballisticInfo);
                }
            }
            return ballisticCollection;
        };

        own.calculateDangerZone = function(shotInfo) {
            if (!own.canCalculate(shotInfo))
                return null;


            var atm = null;

            if (check(shotInfo.getAtmosphere())) {
                var atmoArgs = new b.atmoArguments();
                atmoArgs.temperature = shotInfo.getAtmosphere().getTemperature().get(b.temperatureUnits.Fahrenheit);
                atmoArgs.pressure = shotInfo.getAtmosphere().getPressure().get(b.pressureUnits.InchHg);
                atmoArgs.humidity = shotInfo.getAtmosphere().getHumidity();
                atmoArgs.altitude = shotInfo.getAtmosphere().getAltitude().get(b.distanceUnits.Foot);
                atm = new b.atmo(atmoArgs);
            } else {
                atm = new b.atmo(undefined);
            }

            var bullet = new b.bullet(shotInfo.getAmmo().getBallisticCoefficient(), shotInfo.getAmmo().getDragTable());
            var rifle = new b.rifle(shotInfo.getAmmo().getMuzzleVelocity().get(b.velocityUnits.FeetPerSecond), shotInfo.getSightHeight().get(b.distanceUnits.Foot));

            var wind = null;

            var shot = new b.shot(check(shotInfo.getShotAngle()) ? shotInfo.getShotAngle().get(b.angleUnits.Radian) : 0, check(shotInfo.getCantAngle()) ? shotInfo.getCantAngle().get(b.angleUnits.Radian) : 0, 0);

            var zero = shotInfo.getZeroDistance().get(b.distanceUnits.Foot);
            var targetSize = shotInfo.getTargetSize().get(b.distanceUnits.Foot);

            var jbmArguments = new b.jbmCalculator.calculationArguments();
            jbmArguments.bullet = bullet;
            jbmArguments.rifle = rifle;
            jbmArguments.atmo = atm;
            jbmArguments.shot = shot;
            jbmArguments.wind = wind;
            jbmArguments.zeroRange = 0;
            jbmArguments.nearZero = shotInfo.getNearZero();
            jbmArguments.rangeToOrSize = targetSize;
            jbmArguments.step = 0;
            jbmArguments.target = b.trajectoryTarget.DangerZone;

            var ballistic = b.jbmCalculator.calculate(jbmArguments);

            if (ballistic == null)
                return null;

            var collection = new b.ballisticCollection();

            var bulletWeightGr = shotInfo.getAmmo().getBulletWeight().get(b.weightUnits.Grain);
            var stablityCoefficient = 0;
            if (check(shotInfo.getDriftInfo()))
                stablityCoefficient = b.driftInfoController.calculateStabilityCoefficient(shotInfo.getAmmo(), shotInfo.getDriftInfo(), shotInfo.getAtmosphere());

            for (var i = 0; i < ballistic.ranges.length; i++) {
                if (check(ballistic.ranges[i])) {
                    var v = ballistic.ranges[i].getVelocity();
                    var wi = ballistic.ranges[i].getWindage();
                    var si = 0;
                    if (shotInfo.getDriftInfo() != null)
                        si = b.driftInfoController.calculateDrift(shotInfo.getDriftInfo(), stablityCoefficient, ballistic.ranges[i].getTime()).get(b.distanceUnits.Foot);
                    wi += si;


                    var range = new b.distance(ballistic.ranges[i].getRange(), b.distanceUnits.Foot);
                    var path = new b.distance(ballistic.ranges[i].getDrop(), b.distanceUnits.Foot);
                    var hold = new b.angle(Math.atan(ballistic.ranges[i].getDrop() / ballistic.ranges[i].getRange()), b.angleUnits.Radian);
                    var velocity = new b.velocity(v, b.velocityUnits.FeetPerSecond);
                    var windage = new b.distance(wi, b.distanceUnits.Foot);
                    var windageCorrection = new b.angle(Math.atan(wi / ballistic.ranges[i].getRange()), b.angleUnits.Radian);
                    var ogv = new b.weight(Math.pow(v, 3) * Math.pow(bulletWeightGr, 2) * (1.5e-12), b.weightUnits.Pound);
                    var energy = new b.energy(bulletWeightGr * v * v / 450400, b.energyUnits.FootPounds);
                    var holdClicks = 0, windageClicks = 0;
                    if (check(shotInfo.getHorizontalClick()) && check(shotInfo.getHorizontalClick().get(b.angleUnits.Mil)))
                        windageClicks = (1) * Math.round(windageCorrection.get(b.angleUnits.Mil) / shotInfo.getHorizontalClick().get(b.angleUnits.Mil));
                    if (check(shotInfo.getVerticalClick()) && check(shotInfo.getVerticalClick().get(b.angleUnits.Mil)))
                        holdClicks = (1) * Math.round(hold.get(b.angleUnits.Mil) / shotInfo.getVerticalClick().get(b.angleUnits.Mil));

                    var bInfoArgs = new b.ballisticInfoArguments();
                    bInfoArgs.path = path;
                    bInfoArgs.range = range;
                    bInfoArgs.hold = hold;
                    bInfoArgs.time = ballistic.ranges[i].getTime();
                    bInfoArgs.windage = windage;
                    bInfoArgs.windageCorrection = windageCorrection;
                    bInfoArgs.bulletVelocity = velocity;
                    bInfoArgs.mach = ballistic.ranges[i].getMach();
                    bInfoArgs.bulletEnergy = energy;
                    bInfoArgs.optimalGameWeight = ogv;
                    bInfoArgs.holdClicks = holdClicks;
                    bInfoArgs.windageClicks = windageClicks;

                    var ballisticInfo = new b.ballisticInfo(bInfoArgs);
                    collection.add(ballisticInfo);
                }
            }
            return collection;
        }

    }
    /***********************Main calculation*********************/
    /******************Ballistic Controller****************/
    b.ballisticInfoArguments = function() {
        var own = this;

        own.path = null;
        own.range = null;
        own.hold = null;
        own.time = null;
        own.windage = null;
        own.windageCorrection = null;
        own.bulletVelocity = null;
        own.mach = null;
        own.bulletEnergy = null;
        own.optimalGameWeight = null;
        own.holdClicks = null;
        own.windageClicks = null;

    };

    b.ballisticInfo = function(bArgs) {
        var own = this;

        if (!check(bArgs)) throw "Ballistic info arguments are empty or undefined";
        if (!bArgs instanceof b.ballisticInfoArguments) throw "Ballistic arguments is not instance of Ballistic arguments";
        if (check(bArgs.path) && !bArgs.path instanceof b.distance) throw "Passed path argument is not instance of Ballistic distance";
        if (check(bArgs.range) && !bArgs.range instanceof b.distance) throw "Passed range argument is not instance of Ballistic distance";
        if (check(bArgs.hold) && !bArgs.hold instanceof b.angle) throw "Passed hold argument is not instance of Ballistic angle";
        if (!checkNumeric(bArgs.time)) throw "Passed time argument is not correct";
        if (check(bArgs.windage) && !bArgs.windage instanceof b.distance) throw "Passed windage argument is not instance of Ballistic distance";
        if (check(bArgs.windageCorrection) && !bArgs.windageCorrection instanceof b.angle) throw "Passed windageCorrection argument is not instance of Ballistic angle";
        if (check(bArgs.bulletVelocity) && !bArgs.bulletVelocity instanceof b.velocity) throw "Passed bulletVelocity argument is not instance of Ballistic velocity";
        if (!checkNumeric(bArgs.mach)) throw "Passed mach argument is incorrect";
        if (check(bArgs.bulletEnergy) && !bArgs.bulletEnergy instanceof b.energy) throw "Passed bulletEnergy argument is not instance of Ballistic energy";
        if (check(bArgs.optimalGameWeight) && !bArgs.optimalGameWeight instanceof b.weight) throw "Passed optimalGameWeight argument is not instance of Ballistic weight";
        if (!checkNumeric(bArgs.holdClicks)) throw "Passed incorrect holdClicks argument";
        if (!checkNumeric(bArgs.windageClicks)) throw "Passed incorrect windageClicks argument";

        var path = bArgs.path;
        var range = bArgs.range;
        var hold = bArgs.hold;
        var time = bArgs.time;
        var windage = bArgs.windage;
        var windageCorrection = bArgs.windageCorrection;
        var bulletVelocity = bArgs.bulletVelocity;
        var mach = bArgs.mach;
        var bulletEnergy = bArgs.bulletEnergy;
        var optimalGameWeight = bArgs.optimalGameWeight;
        var holdClicks = bArgs.holdClicks;
        var windageClicks = bArgs.windageClicks;


        own.setPath = function(pathArg) {
            path = pathArg;
        };

        own.getPath = function() {
            return path;
        };

        own.setRange = function(rangeArg) {
            range = rangeArg;
        };

        own.getRange = function() {
            return range;
        };

        own.setHold = function(holdArg) {
            hold = holdArg;
        };

        own.getHold = function() {
            return hold;
        };

        own.setTime = function(timeArg) {
            time = timeArg;
        };

        own.getTime = function() {
            return time;
        };

        own.setWindage = function(windageArg) {
            windage = windageArg;
        };

        own.getWindage = function() {
            return windage;
        };

        own.setWindageCorrection = function(windageCorrectionArg) {
            windageCorrection = windageCorrectionArg;
        };

        own.getWindageCorrection = function() {
            return windageCorrection;
        };

        own.setBulletVelocity = function(bulletVelocityArg) {
            bulletVelocity = bulletVelocityArg;
        };

        own.getBulletVelocity = function() {
            return bulletVelocity;
        };

        own.setMach = function(machArg) {
            mach = machArg;
        };

        own.getMach = function() {
            return mach;
        };

        own.setBulletEnergy = function(bulletEnergyArg) {
            bulletEnergy = bulletEnergyArg;
        };

        own.getBulletEnergy = function() {
            return bulletEnergy;
        };

        own.setOptimalGameWeight = function(optimalGameWeightArg) {
            optimalGameWeight = optimalGameWeightArg;
        };

        own.getOptimalGameWeight = function() {
            return optimalGameWeight;
        };

        own.setHoldClicks = function(holdClicksArg) {
            holdClicks = holdClicksArg;
        };

        own.getHoldClicks = function() {
            return holdClicks;
        };

        own.setWindageClicks = function(windageClicksArg) {
            windageClicks = windageClicksArg;
        };

        own.getWindageClicks = function() {
            return windageClicks;
        };
    };

    b.ballisticCollection = function() {
        var own = this;
        var name = null;
        var collection = new Array();

        own.setCollection = function(collectionArg) {
            if (!check(collectionArg) || !collectionArg instanceof  Array) throw "Passed collection is incorrect";
            collection = collectionArg;
        };

        own.getCollection = function() {
            return collection;
        };

        own.setName = function (nameArg) {
            name = nameArg;
        };

        own.getName = function () {
            return name;
        };

        own.get = function(index) {
            return collection[index];
        };

        own.add = function(ballisticInfo) {
            if (!check(ballisticInfo) || !ballisticInfo instanceof b.ballisticInfo) throw "Passed incorrect ballistic info argument";
            collection.push(ballisticInfo);
        };

        own.count = function() {
            if (check(collection)) {
                return collection.length;
            } else {
                return 0;
            }
        };
    };

    function ballisticInfoController() {
        var own = this;

        own.isBetween = function(v, r1, r2) {
            if (r1 > r2) {
                var x;
                x = r2;
                r2 = r1;
                r1 = x;
            }
            return v >= r1 && v <= r2;
        };

        own.getDistanceByHold = function(ballisticInfoColl, hold, foundLast) {
            var own = this;
            var returnObject = {};
            returnObject.isFound = false;
            returnObject.distance = null;

            if (!check(ballisticInfoColl) || !check(hold)) {
                returnObject.isFound = false;
                return returnObject;
            }

           var holdInRadian = hold.get(b.angleUnits.Radian);

            for (var i = 0; i < ballisticInfoColl.count(); i++) {
                var h1, h2;
                h1 = ballisticInfoColl[i].getHold().get(b.angleUnits.Radian);
                h2 = ballisticInfoColl[i + 1].getHold().get(b.angleUnits.Radian);
                if (own.isBetween(holdInRadian, h1, h2))
                {
                    var d, d1, d2;
                    d1 = ballisticInfoColl[i].getRange().get(b.distanceUnits.Yard);
                    d2 = ballisticInfoColl[i + 1].getRange().get(b.distanceUnits.Yard);
                    d = d1 + ((holdInRadian - h1) / (h2 - h1)) * (d2 - d1);
                    returnObject.distance = new b.distance(d, b.distanceUnits.Yard);
                    returnObject.isFound = true;
                    if (!foundLast)
                        break;
                }
            }

            return returnObject;
        };

        own.getHoldAndAdjustmentByDistance = function(ballisticInfoColl, distance, foundLast) {
            var own = this;
            var returnObject = {};

            returnObject.hold = null;
            returnObject.windage = null;
            returnObject.isFound = false;

            if (!check(ballisticInfoColl) || !check(distance)) {
                returnObject.isFound = false;
            }

            var distanceInYard = distance.get(b.distanceUnits.Yard);
            returnObject.isFound = false;
            for (var i = 0; i < ballisticInfoColl.count(); i++) {
                var d1, d2;
                d1 = ballisticInfoColl[i].getRange().get(b.distanceUnits.Yard);
                d2 = ballisticInfoColl[i + 1].getRange().get(b.distanceUnits.Yard);
                if (own.isBetween(distanceInYard, d1, d2)) {
                    var h, h1, h2;
                    var w, w1, w2;

                    h1 = ballisticInfoColl[i].getHold().get(b.angleUnits.Radian);
                    h2 = ballisticInfoColl[i + 1].getHold().get(b.angleUnits.Radian);
                    h = h1 + ((distanceInYard - d1) / (d2 - d1)) * (h2 - h1);
                    returnObject.hold = new b.angle(h, b.angleUnits.Radian);

                    w1 = ballisticInfoColl[i].getWindageCorrection().get(b.angleUnits.Radian);
                    w2 = ballisticInfoColl[i + 1].getWindageCorrection().get(b.angleUnits.Radian);
                    w = w1 + ((distanceInYard - d1) / (d2 - d1)) * (w2 - w1);
                    returnObject.windage = new b.angle(w, b.angleUnits.Radian);
                    returnObject.isFound = true;
                    if (!foundLast)
                        break;
                }
            }
            return returnObject;
        };
    }


    /***************DRIFT INFO**************/

    b.driftInfoArguments = function () {
        var own = this;
        own.bulletLength = null;
        own.bulletDiameter = null;
        own.riflingTwist = null;
        own.rightHandTwist = null;
    };

    b.driftInfo = function(driftInfoArgs) {
        var own = this;
        var bulletLength = null;
        var bulletDiameter = null;
        var riflingTwist = null;
        var rightHandTwist = null;

        if (check(driftInfoArgs)) {
            if (!driftInfoArgs instanceof b.driftInfoArguments) throw "Passed incorrect drift info argument";
            if (check(driftInfoArgs.bulletLength) && !driftInfoArgs.bulletLength instanceof b.distance) throw "Passed incorrect bullet length";
            if (check(driftInfoArgs.bulletDiameter) && !driftInfoArgs.bulletDiameter instanceof b.distance) throw "Passed incorrect bullet diameter";
            if (check(driftInfoArgs.riflingTwist) && !driftInfoArgs.riflingTwist instanceof b.distance) throw "Passed incorrect rifling twist";
            if (!check(driftInfoArgs.rightHandTwist)) throw "Passed incorrect right hand twist";
            bulletLength = driftInfoArgs.bulletLength;
            bulletDiameter = driftInfoArgs.bulletDiameter;
            riflingTwist = driftInfoArgs.riflingTwist;
            rightHandTwist = driftInfoArgs.rightHandTwist;
        }

        own.setBulletLength = function(bulletLengthArg) {
            bulletLength = bulletLengthArg;
        };

        own.getBulletLength = function() {
           return bulletLength;
        };

        own.setBulletDiameter = function(bulletDiameterArg) {
            bulletDiameter = bulletDiameterArg;
        };

        own.getBulletDiameter = function() {
            return bulletDiameter;
        };

        own.setRiflingTwist = function(riflingTwistArg) {
            riflingTwist = riflingTwistArg;
        };

        own.getRiflingTwist = function () {
            return riflingTwist;
        };

        own.setRightHandTwist = function(rightHandTwistArg) {
            rightHandTwist = rightHandTwistArg;
        };

        own.isRightHandTwist = function() {
            return rightHandTwist;
        };

    };

    b.driftInfoController = function() {
        var own = this;

        own.calculateStabilityCoefficient = function(ammoInfo, driftInfo, atmosphere) {

            var w = ammoInfo.getBulletWeight().get(b.weightUnits.Grain);
            var t = driftInfo.getRiflingTwist().get(b.distanceUnits.Inch) / driftInfo.getBulletDiameter().get(b.distanceUnits.Inch);
            var d = driftInfo.getBulletDiameter().get(b.distanceUnits.Inch);
            var l = driftInfo.getBulletLength().get(b.distanceUnits.Inch) / driftInfo.getBulletDiameter().get(b.distanceUnits.Inch);
            var sd = 30 * w / (Math.pow(t, 2) * Math.pow(d, 3) * l * (1 + Math.pow(l, 2)));

            //correct for muzzle velocity
            var fv = Math.pow(ammoInfo.getMuzzleVelocity().get(b.velocityUnits.FeetPerSecond) / 2800, 1.0 / 3.0);
            var ftp = 1;

            if (atmosphere != null) {
                var ft = atmosphere.getTemperature().get(b.temperatureUnits.Fahrenheit);
                var pt = atmosphere.getPressure().get(b.pressureUnits.InchHg);
                ftp = ((ft + 460) / (59 + 460)) * (29.92 / pt);
            }

            return sd * fv * ftp;
        };

        own.calculateDrift = function(driftInfo, stabilityCoefficient, timeOfFly) {
            return new b.distance(1.25 * (stabilityCoefficient + 1.2) * Math.pow(timeOfFly, 1.83) * (driftInfo.isRightHandTwist() ? -1 : 1) , b.distanceUnits.Inch);
        };
    };

    /***************Help Functions**********/

    function checkVector(vector) {
        if (!vector instanceof b.vector) throw "Passed vector is not instance of Ballistic vector";
        if (!checkNumeric(vector.x) ||
            !checkNumeric(vector.y) ||
            !checkNumeric(vector.z)) throw "Vector coordinates are incorrect or undefined";
    }

    function Polymorph() {
        var len2func = [];
        for(var i=0; i < arguments.length; i++)
            if(typeof(arguments[i]) === "function")
                len2func[arguments[i].length] = arguments[i];
        return function() {
            return len2func[arguments.length].apply(this, arguments);
        }
    }


    function toFixed(value, precision) {
        var precision = precision || 0,
            power = Math.pow(10, precision),
            absValue = Math.abs(Math.round(value * power)),
            result = (value < 0 ? '-' : '') + String(Math.floor(absValue / power));

        if (precision > 0) {
            var fraction = String(absValue % power),
                padding = new Array(Math.max(precision - fraction.length, 0) + 1).join('0');
            result += '.' + padding + fraction;
        }
        return result;
    }

    /*
     * Function is developed for parsing passed unit
     *
     */
    function parseUnit(unit, unitTypeObject) {
        if (!check(unit)) throw "Parse unit exception: passed unit value is undefined";
        if (!check(unitTypeObject)) throw "Parse unit exception: passed unit type is undefined";
        var unitTypes = Object.keys(unitTypeObject);
        if (!check(unitTypes)) throw "Parse unit exception: unit types array is undefined";
        var unitsLength = unitTypes.length;
        if (!check(unitsLength) || unitsLength == 0) throw "Parse unit exception: passed unit type is empty";
        var parsedUnit = undefined;

        for (var i = 0; i < unitsLength; i++) {
            if (unitTypeObject[unitTypes[i]] === unit) {
                parsedUnit = unitTypeObject[unitTypes[i]];
                break;
            }
        }
        return parsedUnit;
    }

    function check(data) {
        return ((typeof (data) !== 'undefined') && (data !== null));
    }

    function checkNumeric(data) {
        return check(data);
    }

    function checkBool(data) {
        return check(data);
    }
    /**********************Instances************************/
    b.vectorLogic = new vectorLogic();
    b.jbmCalculator = new jbmCalculator();
    b.weightLogic = new weightLogic();
    b.distanceLogic = new distanceLogic();
    b.velocityLogic = new velocityLogic();
    b.angleLogic = new angleLogic();
    b.pressureLogic = new pressureLogic();
    b.temperatureLogic = new temperatureLogic();
    b.energyLogic = new energyLogic();
    b.shotInfoController = new shotInfoController();
    b.ballisticInfoController = new ballisticInfoController();
    b.driftInfoController = new driftInfoController();

};