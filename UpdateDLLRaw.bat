DEL "%1\Assemblies\AK_Ability.dll"
COPY "%2\AKA_Ability\bin\Debug\AKA_Ability.dll" "%1\Assemblies\"
REN "%1\Assemblies\AKA_Ability.dll" "AK_Ability.dll"

ECHO "----Shield Frame"
DEL "%1\Assemblies\AKS_Shield.dll"
COPY "%2\AKS_ShieldFramework\obj\Debug\AKS_Shield.dll" "%1\Assemblies\"

DEL "%1\Assemblies\1AK_TypeDef.dll"
COPY "%2\AK_TypeDef\bin\Debug\1AK_TypeDef.dll" "%1\Assemblies\"

DEL "%1\Assemblies\2AKR_WeightedRandomUnified.dll"
COPY "%2\AKR_WeightedRandomUnified\bin\Debug\2AKR_WeightedRandomUnified.dll" "%1\Assemblies\"

DEL "%1\Assemblies\Astrologer.dll"
COPY "S:\Program Files (x86)\Steam\steamapps\common\RimWorld\Mods\__StarSeeker\Source\bin\Debug\Astrologer.dll" "%1\Assemblies\"
PAUSE