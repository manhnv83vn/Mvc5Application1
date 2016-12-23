var common = (function (window, undefined) {
    "use strict";

    var getAbsoluteUrl = function (relativeUrl) {
        return window.__appConfig.baseUrl + relativeUrl;
    };

    var updateUi = function (relativeUrl, uiElement) {
        return $.ajax({
            url: relativeUrl,
            type: "GET"
        }).done(function (data) {
            $(uiElement).html(data);
        });
    };

    var showStaticPopup = function (uiElment, closeCallback) {
        $(uiElment).modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });

        $(uiElment).modal('show');
        $(uiElment).on('hidden.bs.modal', function () {
            if (closeCallback && typeof (closeCallback) === "function") {
                closeCallback();
            }
        });

        $(uiElment).draggable({
            handle: ".line-panel.bg-cyan.row"
        });
    };

    var showPopup = function (relativeUrl, uiElment, closeCallback) {
        updateUi(relativeUrl, uiElment);
        $(uiElment).modal({
            backdrop: 'static',
            keyboard: true,
            show: true
        });

        $(uiElment).modal('show');
        $(uiElment).on('hidden.bs.modal', function (e) {
            if (closeCallback && typeof (closeCallback) === "function") {
                closeCallback();
            }
        });

        $(uiElment).draggable({
            handle: ".line-panel.bg-cyan.row"
        });
    };

    var hidePopup = function (uiElement) {
        $(uiElement).modal('hide');
    };

    var setSelectStyle = function () {
        $(document).ready(function () {
            $('select:not([disabled], [readonly])').selectpicker();
        });
    };

    var setHowDisplayBottom = function () {
        if ($(window).height() >= $(document).height()) {
            $('.footer').addClass('navbar-fixed-bottom');
        } else {
            $('.footer').removeClass('navbar-fixed-bottom');
        }
    };

    var setDisplayForEntry = function () {
        //Add form-control class to fields
        $('.form-group input[type="text"]').addClass("form-control");
        $('.form-group select').addClass("form-control");

        //Add folowed class to required fields
        $('.form-group select[data-val-required]').addClass("form-control followed");
        $('.form-group input[type="text"][data-val-required]').addClass("form-control followed");
        $('.form-group textarea[data-val-required]').addClass("form-control followed");

        //Add HTML attribute spellcheck="true" to all free text fields
        $('.form-group textarea').attr("spellcheck", "true");
    };

    var htmlDecode = function (str) {
        return $('<div/>').html(str).text();
    };

    function startLoading(loadingSelector) {
        loadingSelector.empty();
        $(loadingSelector).append('<div id="LoadingDiv" class="center"><img src="' + window.__appConfig.baseUrl + 'Images/icons/loading.gif" /></div>');
    }

    function endLoading(loadingSelector) {
        loadingSelector.empty();
    }

    var setDisplayDatatable = function (selector, isShowNoRecords) {
        $(document).ready(function () {
            if (selector == null) {
                selector = '.table-responsive table';
            }
            if (isShowNoRecords == null) {
                $(selector).dataTable({
                    "sPaginationType": "full_numbers",
                    "bFilter": false,
                    "bLengthChange": false,
                    "aaSorting": []
                });
            } else if (isShowNoRecords) {
                $(selector).dataTable({
                    "sPaginationType": "full_numbers",
                    "bFilter": false,
                    "bLengthChange": false,
                    "aaSorting": [1, 'asc'],
                    "aoColumnDefs": [
                        { 'bSortable': false, 'aTargets': [0] }
                    ]
                });
            } else {
                $(selector).dataTable({
                    "sPaginationType": "full_numbers",
                    "bFilter": false,
                    "bLengthChange": false
                });
            }
        });
    };

    var objectFindByKey = function (array, key, value) {
        for (var i = 0; i < array.length; i++) {
            if (array[i][key] == value) {
                return array[i];
            }
        }
        return null;
    };

    var convertJSONStringtoDateTime = function (value) {
        if (value != undefined) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            if (results != undefined) {
                var dt = new Date(parseFloat(results[1]));
                return dt;
            }
            return value;
        }
        return new Date(0);
    };

    var convertMMMDateTime = function (value) {
        if (value != undefined) {
            var pattern = /Date\(([^)]+)\)/;
            var results = pattern.exec(value);
            if (results === null) {
                return value;
            }
            var dt = new Date(parseFloat(results[1]));
            var month = new Array();
            month[0] = "Jan";
            month[1] = "Feb";
            month[2] = "Mar";
            month[3] = "Apr";
            month[4] = "May";
            month[5] = "Jun";
            month[6] = "Jul";
            month[7] = "Aug";
            month[8] = "Sep";
            month[9] = "Oct";
            month[10] = "Nov";
            month[11] = "Dec";
            var n = month[dt.getMonth()];
            return ('0' + dt.getDate()).slice(-2) + "-" + n + "-" + dt.getFullYear();
        }
        return new Date(0);
    };

    var convertIntegerFromDate = function (ddMMMyyyy) {
        var monthArray = new Array();
        monthArray[11] = "Jan";
        monthArray[12] = "Feb";
        monthArray[13] = "Mar";
        monthArray[14] = "Apr";
        monthArray[15] = "May";
        monthArray[16] = "Jun";
        monthArray[17] = "Jul";
        monthArray[18] = "Aug";
        monthArray[19] = "Sep";
        monthArray[20] = "Oct";
        monthArray[21] = "Nov";
        monthArray[22] = "Dec";

        var splitDate = ddMMMyyyy.split('-');
        if (splitDate.length !== 3) return 0;
        var day = "" + splitDate[0];
        var month = "" + monthArray.indexOf(splitDate[1]);
        var year = "" + splitDate[2];
        return parseInt(year + month + day);
    }

    var wrapText = function (text, maxChars) {
        if (text != undefined) {
            return "";
        }
        var ret = [];
        var words = text.split(/\b/);

        var currentLine = '';
        var lastWhite = '';
        words.forEach(function (d) {
            var prev = currentLine;
            currentLine += lastWhite + d;

            var l = currentLine.length;

            if (l > maxChars) {
                ret.push(prev.trim());
                currentLine = d;
                lastWhite = '';
            } else {
                var m = currentLine.match(/(.*)(\s+)$/);
                lastWhite = (m && m.length === 3 && m[2]) || '';
                currentLine = (m && m.length === 3 && m[1]) || currentLine;
            }
        });

        if (currentLine) {
            ret.push(currentLine.trim());
        }

        return ret.join("\n");
    };

    var resetFormValidation = function (formElement) {
        $(formElement).validate().resetForm();

        //reset unobtrusive validation summary, if it exists
        $(formElement).find("[data-valmsg-summary=true]")
            .removeClass("validation-summary-errors")
            .addClass("validation-summary-valid")
            .find("ul").empty();

        //reset unobtrusive field level, if it exists
        $(formElement).find("[data-valmsg-replace]")
            .removeClass("field-validation-error")
            .addClass("field-validation-valid")
            .empty();
    };

    var randomIntFromInterval = function (min, max) {
        return Math.floor(Math.random() * (max - min + 1) + min);
    }

    Array.prototype.remove = function (val) {
        var i = this.indexOf(val);
        return i > -1 ? this.splice(i, 1) : [];
    };

    var getParameterByName = function (name) {
        name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
        var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
            results = regex.exec(location.search);
        return results === null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
    };

    return {
        updateUi: updateUi,
        getAbsoluteUrl: getAbsoluteUrl,
        showPopup: showPopup,
        showStaticPopup: showStaticPopup,
        setSelectStyle: setSelectStyle,
        setHowDisplayBottom: setHowDisplayBottom,
        setDisplayForEntry: setDisplayForEntry,
        htmlDecode: htmlDecode,
        startLoading: startLoading,
        endLoading: endLoading,
        setDisplayDatatable: setDisplayDatatable,
        hidePopup: hidePopup,
        objectFindByKey: objectFindByKey,
        convertJSONStringtoDateTime: convertJSONStringtoDateTime,
        convertMMMDateTime: convertMMMDateTime,
        wrapText: wrapText,
        resetFormValidation: resetFormValidation,
        randomIntFromInterval: randomIntFromInterval,
        convertIntegerFromDate: convertIntegerFromDate,
        getParameterByName: getParameterByName
    };
})(window);;

function preventBackSpace(event) {
    var doPrevent = false;
    if (event.keyCode === 8) {
        var d = event.srcElement || event.target;
        if ((d.tagName.toUpperCase() === 'INPUT' && (d.type.toUpperCase() === 'TEXT' || d.type.toUpperCase() === 'PASSWORD' || d.type.toUpperCase() === 'FILE' || d.type.toUpperCase() === 'EMAIL'))
                || d.tagName.toUpperCase() === 'TEXTAREA') {
            doPrevent = d.readOnly || d.disabled;
        }
        else {
            doPrevent = true;
        }
    }

    if (doPrevent) {
        event.preventDefault();
    }
}

jQuery.validator.methods["date"] = function (value, element) {
    //always return true as date format is enforced by datepicker
    return true;
};

Number.prototype.round = function (p) {
    p = p || 10;
    return parseFloat(this.toFixed(p));
};

jQuery.extend(jQuery.fn.dataTableExt.oSort, {
    "date-type-pre": function (a) {
        var x;

        if ($.trim(a) !== '') {
            var frDatea = $.trim(a).split(' ');
            var frDatea2 = frDatea[0].split('-');
            var year = frDatea2[2];
            var month = "JanFebMarAprMayJunJulAugSepOctNovDec".indexOf(frDatea2[1]) / 3 + 1;
            var day = frDatea2[0];
            x = (year + month + day);
        }
        else {
            x = Infinity;
        }

        return x;
    },

    "date-type-asc": function (a, b) {
        return a - b;
    },

    "date-type-desc": function (a, b) {
        return b - a;
    }
});