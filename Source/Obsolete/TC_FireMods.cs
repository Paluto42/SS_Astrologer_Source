namespace Astrologer
{
    /*public class TCP_FireModes : CompProperties
    {
        public bool noNormalShot = false;

        public bool noSpecialshot = false;

        public TCP_FireModes()
        {
            compClass = typeof(TC_FireModes);
        }
    }
    public class TC_FireModes : ThingComp
    {
        private Verb verbInt = null;
        private List<FireMode> availableFireModes = new List<FireMode>(Enum.GetNames(typeof(FireMode)).Length);
        private FireMode currentFireModeInt;
        private bool newComp = true;
        public List<FireMode> AvailableFireModes => availableFireModes;
        public TCP_FireModes Props => (TCP_FireModes)props;
        public Thing Caster => Verb.caster;

        public Pawn CasterPawn => Caster as Pawn;
        private Verb Verb
        {
            get
            {
                if (verbInt == null)
                {
                    CompEquippable compEquippable = parent.TryGetComp<CompEquippable>();
                    if (compEquippable != null)
                    {
                        verbInt = compEquippable.PrimaryVerb;
                    }
                    else
                    {
                        Log.ErrorOnce(parent.LabelCap + " has CompFireModes but no CompEquippable", 50020);
                    }
                }
                Log.Message("现在的Verb是: " + verbInt);
                return verbInt;
            }
        }

        public FireMode CurrentFireMode
        {
            get
            {
                return currentFireModeInt;
            }
            set
            {
                currentFireModeInt = value;
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            LongEventHandler.ExecuteWhenFinished(InitAvailableFireModes);
        }

        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref currentFireModeInt, "currentFireMode", FireMode.NormalFire);
            Scribe_Values.Look(ref newComp, "newComp", defaultValue: false);
        }

        public void InitAvailableFireModes()
        {
            availableFireModes.Clear();
            if (!Props.noNormalShot)
            {
                availableFireModes.Add(FireMode.NormalFire);
            }
            if (!Props.noSpecialshot)
            {
                availableFireModes.Add(FireMode.SpecialFire);
            }
            if (availableFireModes != null) 
            {
                foreach (var fireMode in availableFireModes) 
                {
                    Log.Message("availableFireModes列表: " + fireMode);
                }
            }
        }

        public void ToggleFireMode()
        {
            int num = availableFireModes.IndexOf(currentFireModeInt);
            num = (num + 1) % availableFireModes.Count;
            currentFireModeInt = availableFireModes.ElementAt(num);
            Log.Message("现在的开火模式为: " + currentFireModeInt);
        }

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            Log.Message("CompGetGizmosExtra前置");
            if (CasterPawn?.Faction != Faction.OfPlayer)
            {
                yield break;
            }
            foreach (Command item in GenerateGizmos())
            {
                yield return item;
            }
        }

        public IEnumerable<Command> GenerateGizmos()
        {
            Command_Action toggleFireModeGizmo = new()
            {
                action = ToggleFireMode,
                defaultLabel = ("AS_" + currentFireModeInt.ToString() + "Label").Translate(),
                defaultDesc = "AS_ToggleFireModeDesc".Translate(),
                icon = ContentFinder<Texture2D>.Get("UI/Buttons/" + currentFireModeInt),
                //tutorTag = ((availableFireModes.Count > 1) ? "FireModeToggle" : null)
            };
            yield return toggleFireModeGizmo;
        }
    }
    public enum FireMode : byte
    {
        NormalFire,
        SpecialFire
    }
    */
}
