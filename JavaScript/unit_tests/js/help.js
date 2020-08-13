/**
 * Created by VVALITSKIY on 11/3/2015.
 */

var REFERENT_VALUE = 0.000000001;
var ROUNDED_REFERENT_VALUE = 0.01;

function check(data) {
    return ((typeof (data) !== 'undefined') && (data !== null));
}

function drawElevationAngle(result, target, b) {
    if (check(result) && check(result.elevationAngle)&&
        check(target) && target.length > 0) {
        var table = $("<table/>", {
            border : "1|0",
            style : "width:95%; margin: 0 auto; margin-bottom:15px;"
        });
        var tr = $("<tr/>" , {
            class : "trh"
        });
        tr.append("<td>Elevation angle</td>");
        table.append(tr);
        var tr2 = $("<tr/>");
        tr2.append("<td>" + result.elevationAngle.get(b.angleUnits.Radian) + " (rad) </td>");
        table.append(tr2);
        target.append(table);
    }
}


function drawBallisticCollection(result, target, b) {
    if (check(result) && check(result.getCollection()) && result.getCollection().length > 0 &&
        check(target) && target.length > 0) {
            var table = $("<table/>", {
                border: "1|0",
                style: "width:95%; margin: 0 auto;"
            });
            var tr = $("<tr/>", {
                class: "trh"
            });
            tr.append("<td>#</td>");
            tr.append("<td>Range (yd)</td>");
            tr.append("<td>Velocity (ft/s)</td>");
            tr.append("<td>Mach (ft)</td>");
            tr.append("<td>Energy (j)</td>");
            tr.append("<td>Path (in)</td>");
            tr.append("<td>Hold (mdot)</td>");
            tr.append("<td>Hold Clicks ()</td>");
            tr.append("<td>Windage (in)</td>");
            tr.append("<td>Windage. Adj. (mdot)</td>");
            tr.append("<td>Windage Clicks</td>");
            tr.append("<td>Flight time ()</td>");
            tr.append("<td>O.G.W (p)</td>");
            table.append(tr);
            var noResult = true;
            var i = 0;
            var collection = result.getCollection();
            $.each(collection, function (index, elem) {
                if (check(elem)) {
                    noResult = false;
                    var tr2 = $("<tr/>");
                    tr2.append("<td>" + i + " </td>");
                    tr2.append("<td>" + elem.getRange().get(b.distanceUnits.Yard) + "</td>");
                    tr2.append("<td>" + elem.getBulletVelocity().get(b.velocityUnits.FeetPerSecond) + "</td>");
                    tr2.append("<td>" + elem.getMach() + "</td>");
                    tr2.append("<td>" + elem.getBulletEnergy().get(b.energyUnits.FootPounds) + "</td>");
                    tr2.append("<td>" + elem.getPath().get(b.distanceUnits.Inch) + "</td>");
                    tr2.append("<td>" + elem.getHold().get(b.angleUnits.MilDot) + "</td>");
                    tr2.append("<td>" + elem.getHoldClicks() + "</td>");
                    tr2.append("<td>" + elem.getWindage().get(b.distanceUnits.Inch) + "</td>");
                    tr2.append("<td>" + elem.getWindageCorrection().get(b.angleUnits.MilDot) + " </td>");
                    tr2.append("<td>" + elem.getWindageClicks() + " </td>");
                    tr2.append("<td>" + elem.getTime() + "</td>");
                    tr2.append("<td>" + elem.getOptimalGameWeight().get(b.weightUnits.Pound) + "</td>");
                    table.append(tr2);
                    i++;
                }
            });

            if (noResult) {
                target.append("<div>With entered data calculation was not performed. Please open the browser console and see log message.</div>");
            } else {
                target.append(table);
            }
    }
}

function drawRangeTable(result, target) {

    if (check(result) && check(result.ranges) && result.ranges.length > 0 &&
        check(target) && target.length > 0) {
        var table = $("<table/>", {
            border: "1|0",
            style: "width:95%; margin: 0 auto;"
        });
        var tr = $("<tr/>", {
            class: "trh"
        });
        tr.append("<td>#</td>");
        tr.append("<td>Drop</td>");
        tr.append("<td>Mach</td>");
        tr.append("<td>Range</td>");
        tr.append("<td>Time</td>");
        tr.append("<td>Velocity</td>");
        tr.append("<td>Windage</td>");
        table.append(tr);
        var noResult = true;
        var i = 0;
        $.each(result.ranges, function (index, elem) {
            if (check(elem)) {
                noResult = false;
                var tr2 = $("<tr/>");
                tr2.append("<td>" + i + " </td>");
                tr2.append("<td>" + elem.getDrop() + " (ft) </td>");
                tr2.append("<td>" + elem.getMach() + " (ft/s) </td>");
                tr2.append("<td>" + elem.getRange() + " (ft) </td>");
                tr2.append("<td>" + elem.getTime() + " (s) </td>");
                tr2.append("<td>" + elem.getVelocity() + " (ft/s) </td>");
                tr2.append("<td>" + elem.getWindage() + " (ft) </td>");
                table.append(tr2);
                i++;
            }
        });

        if (noResult) {
            target.append("<div>With entered data calculation was not performed. Please open the browser console and see log message.</div>");
        } else {
            target.append(table);
        }
        //$('body').scrollTop("100");
    }
}