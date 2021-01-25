# ComponentManager
This project is a Planetbase mod that allows the users to change some properties of most of the components.

## Requirements
[Unity Mod Installer](https://www.nexusmods.com/site/mods/21)
[Unity Mod Installer Cofiguration](https://www.nexusmods.com/planetbase/mods/43)

## settings.config Sintaxis
Use:

    * CamelCase names.
    * Slash(/) to declare a component.
    * Colon(:) to declare the values.
    * Comma(,) as properties separator.
    * Vertical bar(|) as list's item separator.

Following this rules you should get a result similar to the next structure:

```
/Component:
    Prop1: "Value",
    Prop2: 81
/Component2:
    Prop1: 25,
    ListProp : 
        Ore: 1 | Chicken: 3,
    Prop3: "A rare component"
```

## Resource types
* <details><summary>Vitromeats</summary>

    * AnyMeat
    * Chicken
    * Pork
    * Beef
</details>

* <details><summary>Vegetables</summary>

    * AnyVegetable
    * Wheat
    * Maize
    * Rice
    * Peas
    * Potatoes
    * Lettuce
    * Tomatoes
    * Onions
    * Radishes
    * Mushrooms
</details>

* <details><summary>Meals</summary>

    * Basic
    * Salad
    * Pasta
    * Burger
</details>

* Starch
* Spares
* Semiconductors
* Ore
* Metal
* MedicinalPlants
* MedicalSupplies
* Gun
* Coins
* Bioplastic
* AlcoholicDrink

## Component names

* <details><summary>Plants</summary>

    * MaizePad
	* WheatPad
	* RicePad
	* PeaPad
	* PotatoPad
	* OnionPad
	* LettucePad
    * TomatoPad
	* MushroomPad
	* RadishPad
	* MedicinalPad
	* GmTomatoPad
	* GmOnionPad
    * DecorativePlant
	* PineTree
	* OakTree
</details>

* <details><summary>Processors</summary>

    * MetalProcessor
	* BioplasticProcessor
	* MealMaker
	* TissueSynthesizer
    * DrinksMachine
	* BotWorkshop
	* SparesWorkshop
	* Workbench
	* SemiconductorFoundry
    * ArmsWorkshop
</details>

* Cabinets
    * Armory
    * MedicalCabinet
* Consoles
    * SecurityConsole
    * RadioConsole
    * TelescopeConsole

* <details><summary>Others</summary>

    * Bed
	* SickBayBed
	* Bunk
	* Bench
	* VideoScreen
	* Treadmill
	* ExerciseBar
    * DrinkingFountain
	* Table
	* TableSmall
	* BarTable
	* BarTableNoChairs
    * BotAutoRepair
</details>

## Properties

* <details><summary>Integers</summary>

    * mPowerGeneration
    * mOxygenGeneration
    * mWaterGeneration
    * mMaxUsers
    * mFlags
    * mHoldResourceCount
    * mSurveyedConstructionCount
    * mEmbeddedResourceCount
</details>

* <details><summary>Floats/Decimals</summary>

    * mUsageCooldown
    * mConditionDecayTime
    * mMaxUsageTime
    * mHeight
    * mRepairTime
    * mRadius
    * mResourceProductionPeriod
    * mWallSeparation
</details>

* <details><summary>Strings</summary>

    * mName
    * mPrefabName
    * mTooltip
    * mOperatedModuleType
</details>

* Lists
    * mProduction