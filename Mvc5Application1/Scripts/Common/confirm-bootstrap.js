/* ===================================================
 * confirmModal by Maxime AILLOUD
 * https://github.com/mailloud/confirm-bootstrap
 * ===================================================
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENCE
 *                    Version 2, December 2004
 *
 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 *
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this licence document, and changing it is allowed as long
 * as the name is changed.
 *
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENCE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION
 *
 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * ========================================================== */


(function ($) {
    $.fn.confirmModal = function (opts) {
        var body = $('body');
        var defaultOptions = {
            confirmTitle: 'Please confirm',
            confirmMessage: 'Are you sure you want to perform this action ?',
            confirmOk: 'Yes',
            confirmCancel: 'Cancel',
            confirmDirection: 'rtl',
            confirmStyle: 'primary',
            confirmCallback: defaultCallback,
            confirmCancelCallback: defaultCancelCallback,
            cssClass: '',
            hiddenCancel: false
        };
        var options = $.extend(defaultOptions, opts);
        var time = Date.now();

        var headModalTemplate =
            '<div class="confirm-modal #CssClass# modal fade" id="#modalId#" tabindex="-1" role="dialog" aria-labelledby="#AriaLabel#" aria-hidden="true" data-backdrop="static">' +
                '<div class="modal-dialog">' +
                    '<div class="modal-content">' +
                        '<div class="line-panel bg-cyan row" style="background: #00afd8; color: white; padding: 3px 10px; cursor: move;">' +
                            '<div class="pull-left modal-title" style="">#Heading#</div>' +
                            '<div class="pull-right">' +
                                '<button type="button" class="btn btn-primary" data-dismiss="modal" aria-hidden="true" style="margin: 0">x</button>' +
                            '</div>' +
                        '</div>' +
                        '<div class="modal-body">' +
                            '<div class="content-box validation-summary-errors">' +
                                '<h4>#Body#</h4>' +
                            '</div>' +
                            '<div class="line-panel row bg-prm-blue modal-footer" style="padding: 3px 10px;">' +
                                '<div class="pull-right">#buttonTemplate#</div>' +
                            '</div>' +
                        '</div>' +
                    '</div>' +
                '</div>' +
            '</div>'
        ;

        return this.each(function (index) {
            var confirmLink = $(this);
            var targetData = confirmLink.data();

            var currentOptions = $.extend(options, targetData);

            var modalId = "confirmModal" + parseInt(time + index);
            var modalTemplate = headModalTemplate;
            var buttonTemplate =
                '<button class="btn btn-#Style# btn-primary" style="margin-left: 10px;" data-dismiss="ok">#Ok#</button>';
            if (!options.hiddenCancel) {
                buttonTemplate += '<button class="btn btn-default btn-primary" style="margin-left: 10px;" data-dismiss="modal">#Cancel#</button>';
            }

            if (options.confirmDirection == 'ltr') {
                if (!options.hiddenCancel) {
                    buttonTemplate = '<button class="btn btn-default btn-primary" style="margin-left: 10px;" data-dismiss="modal">#Cancel#</button>';
                } else {
                    buttonTemplate = '';
                }
                buttonTemplate += '<button class="btn btn-#Style# btn-primary" style="margin: 10px;" data-dismiss="ok">#Ok#</button>';
            }

            modalTemplate = modalTemplate.
                replace('#buttonTemplate#', buttonTemplate).
                replace('#modalId#', modalId).
                replace('#AriaLabel#', options.confirmTitle).
                replace('#Heading#', options.confirmTitle).
                replace('#Body#', options.confirmMessage).
                replace('#Ok#', options.confirmOk).
                replace('#Cancel#', options.confirmCancel).
                replace('#Style#', options.confirmStyle).
                replace('#CssClass#', options.cssClass)
            ;

            body.append(modalTemplate);

            var confirmModal = $('#' + modalId);

            confirmLink.on('click', function (modalEvent) {
                modalEvent.preventDefault();
                confirmModal.modal('show');
                confirmModal.draggable({
                    handle: ".line-panel.bg-cyan.row"
                });
            });

            $('button[data-dismiss="ok"]', confirmModal).on('click', function (event) {
                confirmModal.modal('hide');
                options.confirmCallback(confirmLink);
            });

            $('button[data-dismiss="modal"]', confirmModal).on('click', function (event) {
                confirmModal.modal('hide');
                options.confirmCancelCallback();
            });
        });

        function defaultCallback(target) {
            window.location = $(target).attr('href');
        }

        function defaultCancelCallback() {
        }
    };
})(jQuery);