using System;
using UnityEngine;
using Verse;

namespace Astrologer
{
    [StaticConstructorOnStartup]
    public static class AstroMatPool
    {
        public static Material PsychicDevice_Particle;
        public static Material StellarFoundry_A;
        public static Material StellarFoundry_B;
        public static Material StellarFoundry_C;

        private static readonly string PsychicDevice_ParticleTexPath = "Things/Building/LOF_PsychicDevice/PsychicDevice_Particle";
        private static readonly string StellarFoundry_SphereATexPath = "Things/Building/LOF_StellarFoundry/StellarFoundry_SphereA";
        private static readonly string StellarFoundry_SphereBTexPath = "Things/Building/LOF_StellarFoundry/StellarFoundry_SphereB";
        private static readonly string StellarFoundry_SphereCTexPath = "Things/Building/LOF_StellarFoundry/StellarFoundry_SphereC";

        static AstroMatPool()
        {
			try
			{
				InitializeMaterials();
            }
			catch (Exception)
			{
                Log.Error("Astroloer. Critical Error: Materials Initialization Failed");
                throw;
			}
        }

		private static void InitializeMaterials() 
		{
            PsychicDevice_Particle ??= MaterialPool.MatFrom(PsychicDevice_ParticleTexPath, ShaderDatabase.Transparent);
            StellarFoundry_A ??= MaterialPool.MatFrom(StellarFoundry_SphereATexPath, ShaderDatabase.Cutout);
            StellarFoundry_B ??= MaterialPool.MatFrom(StellarFoundry_SphereBTexPath, ShaderDatabase.Cutout);
            StellarFoundry_C ??= MaterialPool.MatFrom(StellarFoundry_SphereCTexPath, ShaderDatabase.Cutout);
        }
    }
}
