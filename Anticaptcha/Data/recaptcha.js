/*

'iMacros example for Recaptcha 2 solving
VERSION BUILD=844 RECORDER=CR
'
URL GOTO=https://antcpt.com/rus/demo-form/recaptcha-2.html
'
' Insert your Anti-Captcha API key here
SET antiCaptchaApiKey YOUR-ANTI-CAPTCHA-API-KEY
'
' Fetch Anti-Captcha API key in TEXTAREA.g-recaptcha-response element
TAG POS=1 TYPE=TEXTAREA ATTR=CLASS:g-recaptcha-response CONTENT={{antiCaptchaApiKey}}
' Or you can place the API key in DIV#anticaptcha-imacros-account-key, it will also work
'URL GOTO=javascript:(function(){var<SP>d=document.getElementById("anticaptcha-imacros-account-key");d||(d=document.createElement("div"),d.innerHTML="{{antiCaptchaApiKey}}",d.style.display="none",d.id="anticaptcha-imacros-account-key",document.body.appendChild(d))})();
'
' Include recaptcha.js file with all the functional
URL GOTO=javascript:(function(){var<SP>s=document.createElement("script");s.src="https://cdn.antcpt.com/imacros_inclusion/recaptcha.js?"+Math.random();document.body.appendChild(s);})();
'
' Further goes the same code as if you use AntiCaptcha extension
'
' Fill the text field with test value
TAG POS=1 TYPE=INPUT:TEXT FORM=NAME:recaptcha_demo_form ATTR=NAME:demo_text CONTENT=iMacros<SP>test<SP>message
'
' Most important part: we wait 120 seconds until an AntiCatcha indicator 
' with class "antigate_solver" gets in additional "solved" class
SET !TIMEOUT_STEP 120
TAG POS=1 TYPE=DIV ATTR=CLASS:"*antigate_solver*solved*"
'
' Send form
TAG POS=1 TYPE=INPUT:SUBMIT FORM=NAME:recaptcha_demo_form ATTR=TYPE:submit

*/

(function() {

    // TODO:
    // Поменять anticaptcha-imacros-account-key на какой-нибудь anticaptcha-account-key,
    // дабы не привязываться к imacros для, например, headless браузеров.
    // С обратной совместимостью, конечно!

    // location.origin polyfill for IE
    if (!window.location.origin) {
        window.location.origin = window.location.protocol + "//"
            + window.location.hostname
            + (window.location.port ? ':' + window.location.port : '');
    }

    /*jquery inclusion>*/
    var s = document.createElement("script");
    s.src = "https://code.jquery.com/jquery-1.12.4.min.js";
    document.body.appendChild(s);
    /*<jquery inclusion*/

    /*anticaptcha inclusion>*/
    var s = document.createElement("script");
    s.src = "https://cdn.antcpt.com/imacros_inclusion/anticaptcha/anticaptcha.js?" + Math.random();
    document.body.appendChild(s);
    /*<anticaptcha inclusion*/

    // myJquery
    /*
    var $$ = function() {
        var myJquery = {};
        myJquery.getById = function(id) {
            return document.getElementById(id);
        }
        myJquery.getByClassName = function(id) {
            var elements = document.getElementsByClassName();
            // todo: do more
        }
        return myJquery;
    }
    */

    function getIframeSiteKey(iframeUrl) {
        return iframeUrl.replace(/.*k=([^&]+)&.*/, '$1');
    }

    var $antigateSolver;
    var $gRecaptchaResponse;
    var $gRecaptchaFrameContainer;

    var taskInProcessOfSolution = false;

    var recaptchaCallbackAlreadyFired = false;

    var checkModulesLoadedInterval = setInterval(function() {

        // console.log('checkModulesLoadedInterval');

        if (typeof Anticaptcha != 'undefined' && typeof jQuery != 'undefined') {
            clearInterval(checkModulesLoadedInterval);
            $.noConflict();
            // console.log(Anticaptcha);

            $antigateSolver = jQuery();
            $gRecaptchaResponse =  jQuery();
            $gRecaptchaFrameContainer = jQuery();

            // new method of getting Anti-Captcha API key
            var ACCOUNT_KEY_HERE = jQuery('#anticaptcha-imacros-account-key').html();

            // old one
            if (!ACCOUNT_KEY_HERE) {
                ACCOUNT_KEY_HERE = jQuery('.g-recaptcha-response').val();
            }

            // console.log('ACCOUNT_KEY_HERE=', ACCOUNT_KEY_HERE);

            // Go, baby!
            // searching for recaptcha every second
            setInterval(function () {
                //document.body.style.backgroundColor = 'orange';

                // console.log('g-recaptcha-response check');

                jQuery('.g-recaptcha-response:not([anticaptured])').each(function () {
                    var $gRecaptchaResponseLocal = jQuery(this); // textarea.g-recaptcha-response

                    $gRecaptchaResponseLocal.show();

                    // find iframe
                    var $recaptchaIframe = $gRecaptchaResponseLocal.parent().find('iframe');
                    if (!$recaptchaIframe.length || !$recaptchaIframe.attr('src')) {
                        return;
                    }

                    // get siteKey
                    var iframeUrl = parseUrl($recaptchaIframe.attr('src'));
                    var siteKey = getIframeSiteKey(iframeUrl.search); //iframeUrl.search.replace(/.*k=([^&]+)&.*/, '$1');
                    if (!siteKey || iframeUrl.search == siteKey) { // Couldn't get parameter K from url
                        return;
                    }

                    // decode and trim possible spaces
                    siteKey = jQuery.trim(decodeURIComponent(siteKey));

                    var stoken = null;
                    if (iframeUrl.search.indexOf('stoken=') != -1) {
                        stoken = iframeUrl.search.replace(/.*stoken=([^&]+)&?.*/, '$1');
                    }

                    // do not try to handle this textarea anymore
                    $gRecaptchaResponseLocal.attr('anticaptured', 'anticaptured');

                    var $gRecaptchaFrameContainerLocal = $gRecaptchaResponseLocal.prev('div');
                    var $gRecaptchaContainerLocal = $gRecaptchaResponseLocal.parent();
                    $gRecaptchaContainerLocal.height('auto');

                    $gRecaptchaContainerLocal.append('<div class="antigate_solver">AntiCaptcha</div>');
                    var $antigateSolverOne = $gRecaptchaContainerLocal.find('.antigate_solver');

                    // Globals
                    // solver messages
                    $antigateSolver = $antigateSolver.add($antigateSolverOne);
                    // solution textarea
                    $gRecaptchaResponse = $gRecaptchaResponse.add($gRecaptchaResponseLocal);
                    // paint blue flag
                    $gRecaptchaFrameContainer = $gRecaptchaFrameContainer.add($gRecaptchaFrameContainerLocal);

                    if (taskInProcessOfSolution) {
                        return;
                    }

                    taskInProcessOfSolution = true;

                    var anticaptcha = Anticaptcha(ACCOUNT_KEY_HERE);

                    // recaptcha key from target website
                    anticaptcha.setWebsiteURL(window.location.origin);
                    anticaptcha.setWebsiteKey(siteKey); // 12345678901234567890123456789012
                    if (stoken) {
                        anticaptcha.setWebsiteSToken(stoken);
                    }
                    anticaptcha.setSoftId(802);

                    $antigateSolver.removeClass('error');

                    // browser header parameters
                    anticaptcha.setUserAgent("Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/52.0.2743.116");

                    anticaptcha.createTaskProxyless(function (err, taskId) {
                        if (err) {
                            $antigateSolver.removeClass().addClass('antigate_solver').addClass('error').text(err.message);
                            console.error(err);
                            return;
                        }

                        $antigateSolver.text('Solving is in process...');
                        $antigateSolver.addClass('in_process');
                        // console.log(taskId);

                        anticaptcha.getTaskSolution(taskId, function (err, taskSolution) {
                            if (err) {
                                $antigateSolver.removeClass().addClass('antigate_solver').addClass('error').text(err.message);
                                console.error(err);
                                return;
                            }

                            $antigateSolver.text('Solved');
                            $antigateSolver.removeClass().addClass('antigate_solver').addClass('solved');

                            $gRecaptchaFrameContainer.append('DONE!');

                            // console.log(taskSolution);
                            $gRecaptchaResponse.val(taskSolution);

                            // CALLBACK HERE

                            if (typeof ___grecaptcha_cfg != 'undefined'
                                && typeof ___grecaptcha_cfg.clients != 'undefined') {
                                var oneVisibleRecaptchaClientKey = null;

                                // I know, I go to hell after this
                                visible_recaptcha_element_search_loop:
                                    for (var i in ___grecaptcha_cfg.clients) {
                                        for (var j in ___grecaptcha_cfg.clients[i]) {
                                            // check if it's a DOM element within IFRAME
                                            if (___grecaptcha_cfg.clients[i][j]
                                                && typeof ___grecaptcha_cfg.clients[i][j].nodeName == 'string'
                                                && typeof ___grecaptcha_cfg.clients[i][j].innerHTML == 'string'
                                                && typeof ___grecaptcha_cfg.clients[i][j].innerHTML.indexOf('iframe') != -1) {

                                                // console.log('That element');
                                                // console.log(___grecaptcha_cfg.clients[i][j]);

                                                // $(___grecaptcha_cfg.clients[0].Fc).is(":visible")

                                                // check element visibility
                                                // $(___grecaptcha_cfg.clients[i][j]).is(":visible")
                                                if (___grecaptcha_cfg.clients[i][j].offsetHeight != 0
                                                    || (___grecaptcha_cfg.clients[i][j].childNodes.length && ___grecaptcha_cfg.clients[i][j].childNodes[0].offsetHeight != 0)
                                                    || ___grecaptcha_cfg.clients[i][j].dataset.size == 'invisible') { // IS VISIBLE for user or IS INVISIBLE type of reCaptcha
                                                    if (oneVisibleRecaptchaClientKey === null) {
                                                        oneVisibleRecaptchaClientKey = i;
                                                        // only one in this level of search
                                                        break;
                                                    } else {
                                                        // console.log('One only one visible recaptcha, break stuff!');
                                                        oneVisibleRecaptchaClientKey = null;
                                                        break visible_recaptcha_element_search_loop;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                // console.log('oneVisibleRecaptchaClientKey=');
                                // console.log(oneVisibleRecaptchaClientKey);

                                if (oneVisibleRecaptchaClientKey !== null) {
                                    recursiveCallbackSearch(___grecaptcha_cfg.clients[oneVisibleRecaptchaClientKey], taskSolution, 1, 2);
                                }
                            }

                            taskInProcessOfSolution = false;
                        });
                    });
                });
            }, 1000);
        }
    }, 200);

    function parseUrl(url)
    {
        var parser = document.createElement('a');
        parser.href = url;

        return parser;

        parser.protocol; // => "http:"
        parser.hostname; // => "example.com"
        parser.port;     // => "3000"
        parser.pathname; // => "/pathname/"
        parser.search;   // => "?search=test"
        parser.hash;     // => "#hash"
        parser.host;     // => "example.com:3000"
    }

    var recursiveCallbackSearch = function(object, solution, currentDepth, maxDepth) {
        if (recaptchaCallbackAlreadyFired) {
            return;
        }

        var passedProperties = 0;

        for (var i in object) {
            // console.log('i=', i);
    //                                    try {
    //                                        if (!object.hasOwnProperty(i)) {
    //                                            continue;
    //                                        }
    //                                    } catch (e) {
    //                                    }

            passedProperties++;

            // do not go farther
            if (passedProperties > 15) {
                break;
            }

            // prevent "Failed to read the 'contentDocument' property" error
            try {
                if (typeof object[i] == 'object' && currentDepth <= maxDepth) { // she said not too deep
                    // console.log('RECURSIVE call for ', i);
                    recursiveCallbackSearch(object[i], solution, currentDepth + 1, maxDepth);
                    if (recaptchaCallbackAlreadyFired) {
                        return;
                    }
                } else if (i == 'callback') {
                    if (typeof object[i] == 'function') {
                        // console.log('CALLBACK ' + i + ' function with param +' + solution);
                        recaptchaCallbackAlreadyFired = true;
                        object[i](solution);
                    } else if (typeof object[i] == 'string' && typeof window[object[i]] == 'function') {
                        // console.log('CALLBACK ' + object[i] + ' global function with param +' + solution);
                        recaptchaCallbackAlreadyFired = true;
                        window[object[i]](solution);
                    }

                    // one callback in this object
                    return;
                }
            } catch (e) {
            }
        }
    }

})();