using System;
using UnityEngine;
using Verse;

namespace Astrologer
{
    [StaticConstructorOnStartup]
    public static class AstroMatPool
    {
        public static Material PsychicDevice_Particle;

        private static readonly string PsychicDevice_ParticleTexPath = "UI/Effect/PsychicDeviceBase";

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
            PsychicDevice_Particle??= MaterialPool.MatFrom(PsychicDevice_ParticleTexPath, ShaderDatabase.Transparent);
		}
    }
}
