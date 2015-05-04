Shader "SKJZ/ShaderlibDemo/ShaderlabDemoCulling" {
	SubShader {
		Pass{
			Material{
				Diffuse(1, 0, 0, 1)
			}

			Lighting On

			//Cull Back | Front | Off
			Cull Off
		}

	}
}
