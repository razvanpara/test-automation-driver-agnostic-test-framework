# This is a web test framework POC
- It is capable of accepting different implementations for browser control
- It supports both ***Selenium*** and ***Playwright*** and can easily switch between them


### Requirements:
- .net core 6.0
- [playwright CLI](https://www.nuget.org/packages/Microsoft.Playwright.CLI)



##Test cases:
All test cases are using [selenium base demo page](https://seleniumbase.io/demo_page) as the application under test

###Tests:
---
- Text Input Field Accepts Input
    1. Go to the webpage
    2. Enter text 'Hello World' into the text input
    3. Assert that the input field's value is equal to 'Hello World'
---
- Select Dropdown Can Select
    1. Go to the webpage
    2. Select the option with value '50%'
    3. Assert that:
    3.1. The selected option text contains '50%'
    3.2. The meter value is the '0.5'
--- 
- Button Color Changes On Click
    1. Go to the webpage
    2. Assert that the button's colour is 'green'
    3. Click the button
    4. Assert that the button's colour is 'purple'
---
- Drag And Drop Test
    1. Go to the webpage
    2. Click the checkbox that enables the draggable elements
    3. Drag the element
    4. Assert that the element's coordinates have changed on the page