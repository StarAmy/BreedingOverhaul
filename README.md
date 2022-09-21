****Mod and README both incomplete***

Breeding Overhaul is a Stardew Valley mod that aims to revamp the vanilla breeding system when used in conjunction with Animal Husbandry Mod (formerly ButcherMod) and Better Farm Animal Variety. Json Assets, Production Framework Mod, and Content Patcher are all also required for full function. It consists of the following sections:

1. Breeding Overhaul, the main project code. It makes the following changes to the base game / Animal Husbandry Mod behavior:

        Disables normal incubator function, requiring the correct type of 'fertilized' egg instead (the item required for each type of animal can be manually set/reconfigured in the config file 'Incubator Data').
        
        Disables normal pregnancy/insemination function, requiring the correct type of 'DNA' in the insemination syringe instead (the item required for inseminating each type of animal and each parent:offspring possibility can be manually set/reconfigured in the config file 'Pregnancy Data').
    
2. [BFAV] Vanilla Males adds male variants to every category of vanilla livestock via Better Farm Animal Variety. Males drop manure as a common drop, and a species-specific item as a rare drop. 
        
        Male Cow = Manure, Cow Horn
        Male Goat = Manure, Goat Horn    
        Male Sheep = Manure, Sheep Horn     
        Male Pig = Manure, Pig Tusk     
        Male Chicken = Manure, Chicken Feather  
        Male Duck = Manure, Duck Feather (the same item as in vanilla) 
        Male Rabbit = Manure, Rabbit Foot (the same item as in vanilla)
        Male Ostrich = Manure, Ostrich Feather
        Male Dinosaur = Manure, Dinosaur Scale

3. [AHM] Vanilla Male Rules adds rules for Animal Husbandry Mod behavior, including number and type of meat drops, for the new male variants. 

        Males drop the same quantity/type of meat and have the same treat preferences as their vanilla/female counterparts.

4. [CP] Vanilla Livestock Tweaks makes some changes to the pre-existing livestock to make them feel more like females and fit in better with this mod, including some configurable texture changes, and 'large egg' production for several of the poultry types. The production for the 'female' (vanilla) variants is now as follows:
        
        White Cow = Milk, Large Milk
        Brown Cow = Milk, Large Milk
        Goat = Goat Milk, Large Goat Milk
        Sheep = Wool
        Pig  = Truffle
        White Chicken = White Egg, Large White Egg
        Brown Chicken = Brown Egg, Large Brown Egg 
        Blue Chicken = Blue Egg, Large Blue Egg
        Void Chicken = Void Egg, Large Void Egg
        Golden Chicken = Golden Egg, Large Golden Egg
        Duck = Duck Egg, Large Duck Egg
        Rabbit = Rabbit Foot
        Ostrich = Ostrich Egg, Large Ostrich Egg    
        Dinosaur = Dinosaur Egg, Large Dinosaur Egg
        
5. [JA] Breeding Overhaul Objects adds all the new objects dropped by 'male' and 'female' variants, as well as the DNA and fertilized eggs they can be crafted or processed into.
        
        Manure 
     
        Cow Horn, Goat Horn, Sheep Horn, Pig Tusk, Chicken Feather, Ostrich Feather, and Dinosaur Scale 
        
        Cow DNA, Goat DNA, Sheep DNA, Pig DNA, Chicken DNA, Duck DNA, Rabbit DNA, Ostrich DNA, and Dinosaur DNA
        
        Blue Egg, Large Blue Egg, Large Duck Egg, Large Golden Egg, Large Void Egg, and Large Ostrich Egg
        
        Fertilized White Egg, Fertilized Brown Egg, Fertilized Blue Egg, Fertilized Void Egg, Fertilized Golden Egg, Fertilized Duck Egg, Fertilized Ostrich Egg, and                   Fertilized Dinosaur Egg each hatch one new coop animal
        
        Fertilized Large White Egg, Fertilized Large Brown Egg, Fertilized Large Blue Egg, Fertilized Large Void Egg, Fertilized Large Golden Egg, Fertilized Large                     Duck Egg, Fertilized Large Ostrich Egg, and Fertilized Large Dinosaur Egg each hatch 1-2 new coop animals
        
        Blue Mayonnaise, Golden Mayonnaise, Ostrich Mayonnaise
        
6. [PFM] Breeding Overhaul Object Rules adds processing rules for all the new objects. 

    The recycling machine now produces:
       
       Manure = Quality Fertilizer
        
        Cow Horn = Cow DNA     
        Goat Horn = Goat DNA
        Sheep Horn = Sheep DNA
        Pig Tusk = Pig DNA
        Chicken Feather = Chicken DNA
        Duck Feather = Duck DNA
        Rabbit Foot = Rabbit DNA
        Ostrich Feather = Ostrich DNA
        Dinosaur Scale = Dinosaur DNA
        
     The mayonnaise machine now produces:
        
        Blue Egg, Large Blue Egg = Blue Mayonnaise
        Golden Egg, Large Golden Egg = Golden Mayonnaise
        Ostrich Egg, Large Ostrich Egg = Ostrich Mayonnaise


So, what does this mean for gameplay? 

Breeding Egg-Laying Animals In-Game (assuming you use default configuration for incubatordata):

                1. Obtain DNA for the correct species - you can buy DNA from Marnie, or process your male animal's unique drops into it at the recycling machine. 
                2. Craft a fertile egg using an egg and a DNA, or a large fertile egg using a large egg and a DNA.
                3. Drop the fertile egg in the incubator just like normal, and wait for it to hatch (now it has a chance to be born male). Large fertile eggs have a                            chance to hatch two new babies if you have space for them.

Breeding Live-Birth Animals In-Game (assuming you use default configuration for pregnancydata):

                1. Obtain DNA for the correct species - you can buy DNA from Marnie, or process your male animal's unique drops into it at the recycling machine. 
                2. Use the DNA in the Animal Husbandry Mod insemination syringe instead of the vanilla drops (milk, etc). 
                3. Wait for your animal to be born through the normal Animal Husbandry Mod process (now it has a chance to be born male). 

A New Quality Fertilizer Source:

                With a handful of males producing manure you now have a steady supply of quality fertilizer. 

You Need a Few Males For Community Center Bundle:

               Duck Feather and Rabbit Foot items are now dropped by male ducks and male rabbits respectively, instead of their vanilla ('female') counterparts, so 
               you will need to plan ahead and get male chicks / kits from Marnie to complete the original, unmixed community center bundle.
