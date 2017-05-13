# Unity-RuntimeLightEditor

## How to use it

Inside the Light Orbiter folder there's a scene called "main" you get a full example from that scene.

To do it Manually:

First, this is the Light Orbiter component's Inspector

![ Light Orbiter's inspector](http://i.imgur.com/Y25NhW1.png)


#### Basic rotation
Create a directional light and add the LightOrbiter Script to it. For each axis you want to rotate create a Slider object in your  UI.
Check the "Allow X/Y/Z rotation" to enable X, Y or Z rotation. When you check an axis you'll be able to set the slider for that axis in the Inspector. Assign the UI sliders and you're good to go. 


#### Color picking

For color picking you'll need to use an external object (https://github.com/judah4/HSV-Color-Picker-Unity) which is included in the third party folder. Add the color picker to your UI, check the "Allow color picking" option and assign the color picker to the object field. 

#### Presets

There's a support for light presets also.  To create a preset, go to Assets -> Create -> Light Orbiter -> Preset. This will create a Preset object in your project explorer. Select the object and the inspector should look something like this:



![Presets inspector](http://i.imgur.com/67E0fwu.png)

You can place a light int the get from light object field and click the "Get from light" button. That'll get all the settings from the light and store them in the preset.  For each preset you'll have to create a button that makes it work, assign the button to the Preset Buttons array in a "one to one" correspondance manner. Like shown in the next image

![One to one correspondance](http://i.imgur.com/SQipTHd.png?1)
