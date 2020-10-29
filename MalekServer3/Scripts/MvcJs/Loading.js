jQuery(function ($) {
	$(document).ajaxSend(function () {
		$("#overlay").fadeIn(300);
	});

});