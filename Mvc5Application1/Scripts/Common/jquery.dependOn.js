(function ($) {
    /**
     * jQuery.fn.dependsOn
     * @version 1.0.1
     * @date September 22, 2010
     * @since 1.0.0, September 19, 2010
     * @package jquery-sparkle {@link http://www.balupton/projects/jquery-sparkle}
     * @author Benjamin "balupton" Lupton {@link http://www.balupton.com}
     * @copyright (c) 2010 Benjamin Arthur Lupton {@link http://www.balupton.com}
     * @license Attribution-ShareAlike 2.5 Generic {@link http://creativecommons.org/licenses/by-sa/2.5/
     */
    $.fn.dependsOn = function (source, value, callback) {
        var $target = $(this),
            $source = $(source),
            source = $source.attr('name') || $source.attr('id');

        // Add Data
        var dependsOnStatus = $target.data('dependsOnStatus') || {};
        dependsOnStatus[source] = false;
        $target.data('dependsOnStatus', dependsOnStatus);

        // Add Event
        $source.change(function () {
            var pass = false;

            // Determine
            if ((value === null) || (typeof value === 'undefined')) {
                // We don't have a value
                if ($source.is(':checkbox,:radio')) {
                    pass = $source.is(':selected:enabled,:checked:enabled');
                }
                else {
                    pass = Boolean($source.val());
                }
            }
            else {
                // We do have a value
                if ($source.is(":checkbox")) {
                    $source.filter(":checkbox:checked:enabled").each(function () {
                        if ($(this).val() == value) {
                            pass = true;
                            return false;
                        }
                    });
                } else if ($source.is(':radio')) {
                    pass = $source.filter(':radio:checked:enabled').val() == value;
                } else {
                    //handles multi-value select inputs as well as single values                    
                    var arr = value.split(',');
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i].trim() == $source.val()) {
                            pass = true;
                            break;
                        }
                    }
                }
            }

            // Update
            var dependsOnStatus = $target.data('dependsOnStatus') || {};
            dependsOnStatus[source] = pass;
            $target.data('dependsOnStatus', dependsOnStatus);

            // Determine
            var passAll = true;
            $.each(dependsOnStatus, function (i, v) {
                if (!v) {
                    passAll = false;
                    return false; // break
                }
            });
            // console.log(dependsOnStatus);
            // Adjust
            var spanSelector = $target.parent().find('span')[0];
            if (!passAll) {
                $target.attr('disabled', 'disabled').addClass('disabled');

                //hide * require icon
                $(spanSelector).removeClass("required-icon");
            }
            else {
                $target.removeAttr('disabled').removeClass('disabled');

                //show * require icon
                $(spanSelector).addClass("required-icon");
            }

            //callback function
            if (callback) {
                callback($target);
            }            
        }).trigger('change');

        // Chain
        return this;
    };
})(jQuery);