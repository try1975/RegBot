const puppeteer = require('puppeteer');
var fs = require('fs');
//const detectFingerprinting = require('./monitorExecution').detectFingerprinting;
//const detectFingerprinting = require('./inject').detectFingerprinting;

(async () => {
  //const browser = await puppeteer.launch({ headless: false });
  const browserWSEndpoint = 'ws://127.0.0.1:9222/devtools/browser/71a04b60-6ffc-4428-a770-1216ab32db66';
  // Use the endpoint to reestablish a connection
  const browser = await puppeteer.connect({browserWSEndpoint});

    const page = await browser.newPage();
    // In your puppeteer script, assuming the preload.js file is in same folder of our script
    //const preloadFile = fs.readFileSync('./inject.js', 'utf8');
    //await page.evaluateOnNewDocument(preloadFile);

    await page.evaluateOnNewDocument((vendor) => {
        Object.defineProperties(navigator, {
            vendor: {
                get() { return vendor; }
            },
        });
    }, 'Vendorishhhe');

    await page.evaluateOnNewDocument((webdriver) => {
        Object.defineProperties(navigator, {
            webdriver: {
                get() { return webdriver; }
            },
        });
    }, false);

    await page.evaluateOnNewDocument((languages) => {
        Object.defineProperties(navigator, {
            languages: {
                get() { return languages; }
            },
        });
    }, ['en-US', 'en', 'de']);

    await page.evaluateOnNewDocument((platform) => {
        Object.defineProperties(navigator, {
            platform: {
                get() { return platform; }
            },
        });
    }, 'Arm343');

    await page.evaluateOnNewDocument((appName) => {
        Object.defineProperties(navigator, {
            appName: {
                get() { return appName; }
            },
        });
    }, 'Arm3!!!!!!43');

    await page.evaluateOnNewDocument(() => {
        //const script = document.currentScript;
        //script.dataset.active = true; // overwrites enabled or not
        //script.dataset.once = true;
        //script.dataset.injected = true;
        const toBlob = HTMLCanvasElement.prototype.toBlob;
        const toDataURL = HTMLCanvasElement.prototype.toDataURL;
        HTMLCanvasElement.prototype.manipulate = function () {
            const { width, height } = this;
            const context = this.getContext('2d');
            const shift = {
                'r': Math.floor(Math.random() * 10) - 5,
                'g': Math.floor(Math.random() * 10) - 5,
                'b': Math.floor(Math.random() * 10) - 5
            };
            const matt = context.getImageData(0, 0, width, height);
            for (let i = 0; i < height; i += Math.max(1, parseInt(height / 10))) {
                for (let j = 0; j < width; j += Math.max(1, parseInt(width / 10))) {
                    const n = ((i * (width * 4)) + (j * 4));
                    matt.data[n + 0] = matt.data[n + 0] + shift.r;
                    matt.data[n + 1] = matt.data[n + 1] + shift.g;
                    matt.data[n + 2] = matt.data[n + 2] + shift.b;
                }
            }
            context.putImageData(matt, 0, 0);
            //if (script.dataset.once === 'true') {
                this.manipulate = () => {
                    //script.dispatchEvent(new Event('called'));
                };
            //}
            //script.dispatchEvent(new Event('called'));
        };
        Object.defineProperty(HTMLCanvasElement.prototype, 'toBlob', {
            value: function () {
                //if (script.dataset.active === 'true') {
                    try {
                        this.manipulate();
                    }
                    catch (e) {
                        console.warn('manipulation failed', e);
                    }
                //}
                return toBlob.apply(this, arguments);
            }
        });
        Object.defineProperty(HTMLCanvasElement.prototype, 'toDataURL', {
            value: function () {
                //if (script.dataset.active === 'true') {
                    try {
                        this.manipulate();
                    }
                    catch (e) {
                        console.warn('manipulation failed', e);
                    }
                //}
                return toDataURL.apply(this, arguments);
            }
        });
    });
    
    //await page.evaluateOnNewDocument(detectFingerprinting);

    await page.goto('about:blank');
    //await page.goto('https://antoinevastel.com/bots');
    await page.goto('https://ipleak.com/full-report/');
    //await page.goto('http://f.vision');

    //const evalResult = await page.evaluate(() => {
    //    return new Promise(resolve => {
    //        setTimeout(() => {
    //            resolve(navigator.monitorFingerprinting);
    //        }, 2000);
    //    });
    //});

    //console.log(evalResult);

    //await browser.close();
})();