const puppeteer = require('puppeteer');

(async () => {
  /*
    const browser = await puppeteer.launch({headless: false});
  // Store the endpoint to be able to reconnect to Chromium
  const browserWSEndpoint = browser.wsEndpoint();

  let page = await browser.newPage();
  await page.goto('https://www.google.com');
  // Disconnect puppeteer from Chromium
  browser.disconnect();
  */
  const browserWSEndpoint = 'ws://127.0.0.1:9222/devtools/browser/c469e44e-7b6b-4f25-842d-62ed497d5551';
  // Use the endpoint to reestablish a connection
  const browser2 = await puppeteer.connect({browserWSEndpoint});
  page = await browser2.newPage();
  await page.goto('https://yandex.ru');
  // Close Chromium
  await browser2.close();
})();