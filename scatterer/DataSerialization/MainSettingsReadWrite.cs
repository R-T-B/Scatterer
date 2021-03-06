using System.Collections;
using System.Collections.Generic;
using KSP;
using KSP.IO;
using UnityEngine;

namespace scatterer
{
	public class MainSettingsReadWrite
	{
		[Persistent]
		public bool autosavePlanetSettingsOnSceneChange=true;
		
		[Persistent]
		public bool disableAmbientLight=false;
		
		[Persistent]
		public bool integrateWithEVEClouds=false;
		
		[Persistent]
		public bool overrideNearClipPlane=false;
		
		[Persistent]
		public float nearClipPlane=0.5f;
		
		[Persistent]
		public bool useOceanShaders = true;
		
		[Persistent]
		public bool shadowsOnOcean = true;
		
		[Persistent]
		public bool oceanSkyReflections = true;
		
		[Persistent]
		public bool oceanRefraction = true;

		[Persistent]
		public bool oceanCaustics = true;
		
		//[Persistent]
		public bool craft_WaveInteractions = false;
		
		[Persistent]
		public bool oceanPixelLights = false;
		
		[Persistent]
		public bool fullLensFlareReplacement = true;
		
		[Persistent]
		public bool sunlightExtinction = true;
		
		[Persistent]
		public bool underwaterLightDimming = true;
		
		[Persistent]
		public bool showMenuOnStart = true;
		
		[Persistent]
		public bool useEclipses = true;
		
		[Persistent]
		public bool useRingShadows = true;
		
		//[Persistent]
		public bool usePlanetShine = false;
		
		[Persistent]
		public bool terrainShadows = true;
		
		[Persistent]
		public float shadowNormalBias=0.4f;
		
		[Persistent]
		public float shadowBias=0.125f;
		
		[Persistent]
		public float shadowsDistance=100000f;
		
		[Persistent]
		public int m_fourierGridSize = 128;

		[Persistent]
		public bool RSSMode = false;

		public void loadMainSettings ()
		{
			UrlDir.UrlConfig[] baseConfigs = GameDatabase.Instance.GetConfigs ("Scatterer_config");
			if (baseConfigs.Length == 0)
			{
				Utils.LogDebug ("No config file found, check your install");
				return;
			}
			
			if (baseConfigs.Length > 1)
			{
				Utils.LogDebug ("Multiple config files detected, check your install");
			}
			
			ConfigNode.LoadObjectFromConfig (this, (baseConfigs [0]).config);
		}
		
		public void saveMainSettingsIfChanged()
		{
			UrlDir.UrlConfig[] baseConfigs = GameDatabase.Instance.GetConfigs ("Scatterer_config");
			
			if (baseConfigs.Length == 0)
			{
				Utils.LogDebug ("No config file found, check your install");
				return;
			}
			
			if (baseConfigs.Length > 1)
			{
				Utils.LogDebug ("Multiple config files detected, check your install");
			}
			
			MainSettingsReadWrite OldConfig = new MainSettingsReadWrite ();
			ConfigNode.LoadObjectFromConfig (OldConfig, (baseConfigs [0]).config);
			
			bool configChanged =
				(OldConfig.autosavePlanetSettingsOnSceneChange != autosavePlanetSettingsOnSceneChange ||
				 OldConfig.disableAmbientLight != disableAmbientLight ||
				 OldConfig.integrateWithEVEClouds != integrateWithEVEClouds ||
				 OldConfig.overrideNearClipPlane != overrideNearClipPlane ||
				 OldConfig.nearClipPlane != nearClipPlane ||
				 OldConfig.useOceanShaders != useOceanShaders ||
				 OldConfig.shadowsOnOcean != shadowsOnOcean ||
				 OldConfig.oceanSkyReflections != oceanSkyReflections ||
				 OldConfig.oceanRefraction != oceanRefraction ||
				 OldConfig.oceanCaustics != oceanCaustics ||
				 OldConfig.oceanPixelLights != oceanPixelLights ||
				 OldConfig.fullLensFlareReplacement != fullLensFlareReplacement ||
				 OldConfig.sunlightExtinction != sunlightExtinction ||
				 OldConfig.underwaterLightDimming != underwaterLightDimming ||
				 OldConfig.showMenuOnStart != showMenuOnStart ||
				 OldConfig.useEclipses != useEclipses ||
				 OldConfig.useRingShadows != useRingShadows ||
				 OldConfig.terrainShadows != terrainShadows ||
				 OldConfig.shadowNormalBias != shadowNormalBias ||
				 OldConfig.shadowBias != shadowBias ||
				 OldConfig.shadowsDistance != shadowsDistance ||
				 OldConfig.m_fourierGridSize != m_fourierGridSize) ||
				 OldConfig.RSSMode != RSSMode;
			
			if (configChanged)
			{
				Utils.LogDebug("Main config changed");
				
				baseConfigs [0].config = ConfigNode.CreateConfigFromObject (this);
				baseConfigs [0].config.name = "Scatterer_config";
				Utils.LogDebug ("Saving settings to: " + baseConfigs [0].parent.url+".cfg");
				baseConfigs [0].parent.SaveConfigs ();
			}
			else
			{
				Utils.LogDebug("No changes to main config, skipping saving.");
			}

		}
	}
}