## Translation

To help translate, follow these instructions.

### Adding a new language (requires Visual Studio 2019 and Multilingual App Toolkit)
- Ensure you have Visual Studio 2019 and the [Multilingual App Toolkit extension](https://marketplace.visualstudio.com/items?itemName=MultilingualAppToolkit.MultilingualAppToolkit-18308).
- Fork and clone this repo.
- Open in VS 2019.
- Right click on the `FluentHub` project.
- Select Multilingual App Toolkit > Add translation language.
    - If you get a message saying "Translation Provider Manager Issue," just click Ok and ignore it. It's unrelated to adding a language.
- Select a language. 
- Once you select a language, new `.xlf` files will be created in the `MultilingualResources` folder.
- Now follow the `Improving an existing language` steps below.

### Improving an existing language (can be done with any text editor)
- Inside the `MultilingualResources` folder, open the `.xlf` of the language you want to translate.
    - You can open using any text editor, or you can use the [Multilingual Editor](https://developer.microsoft.com/windows/develop/multilingual-app-toolkit)
- If you're using a text editor, translate the strings inside the `<target>` node. Then change the `state` property to `translated`.
    ![](https://github.com/jenius-apps/ambie/blob/main/images/text-translate.png?raw=true)
- If you're using the Multilingual Editor, translate the strings inside the `Translation` text field. Make sure to save to preserve your changes.
    ![](https://github.com/jenius-apps/ambie/blob/main/images/toolkit-translate.png?raw=true)
- Once you're done, commit your changes, push to GitHub, and make a pull request.
