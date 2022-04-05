# Translation Guidlines

To help translate, follow these instructions.

## Add localization resources

 1. Ensure you have Visual Studio 2022 for windows and the [Multilingual App Toolkit extension](https://marketplace.visualstudio.com/items?itemName=MultilingualAppToolkit.MultilingualAppToolkit-18308).
 2. Fork and clone this repository.
 3. Open in Visual Studio 2022.
 4. Select Multilingual App Toolkit > Add translation language.
 5. Select a language. 
 6. Once you select a language, new `.xlf` files will be created in the `MultilingualResources` folder.
 7. Follow the `Improve existring localization resources` steps below.

### Improve existring localization resources
 1. Inside the `MultilingualResources` folder, open the `.xlf` of the language you want to translate with [Multilingual Editor](https://developer.microsoft.com/windows/develop/multilingual-app-toolkit).
 2. Translate the strings inside the `Translation` text field. Make sure to save to preserve your changes.

    ![image](https://user-images.githubusercontent.com/62196528/158168158-41653239-1f91-4be8-8518-e45e90ec9af8.png)

 3. After you're done, commit your changes in your branch and make a PR to propose these changes.
