$.validator.addMethod('requiredif',
    function (value, element, parameters) {
        var proName = parameters['dependentproperty'];
        proName = proName.replace(/[!"#$%&'()*+,.\/:;<=>?@\[\\\]^`{|}~]/g, '\\$&');
        var id = '#' + proName;
        var name = '[name="' + proName + '"]';

        var targetvalue = parameters['targetvalue'];
        targetvalue =
          (targetvalue == null ? '' : targetvalue).toString();

        var control = $(id + ", " + name).first();

        var controltype = control.attr('type');
        var actualvalue = controltype === 'checkbox' ? control.prop('checked').toString() : control.val();

        var isValid = true;
        if (targetvalue === actualvalue) {
            isValid = $.trim(value).length > 0;
        }
        return isValid;
    }
);

$.validator.unobtrusive.adapters.add(
    'requiredif',
    ['dependentproperty', 'targetvalue'],
    function (options) {
        options.rules['requiredif'] = {
            dependentproperty: options.params['dependentproperty'],
            targetvalue: options.params['targetvalue']
        };
        options.messages['requiredif'] = options.message;
    });

