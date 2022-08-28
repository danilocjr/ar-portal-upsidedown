using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UnityEngine.XR.iOS
{
	public class PortalController : MonoBehaviour {
		
		private static PortalController _instance;
		public static PortalController Instance { get { return _instance; } }

		public Material[] materials;
        public Material materialFloor;
        public MeshRenderer meshRenderer;
		//public UnityARVideo unityARVideo;

		//public AnimationBehavior animBehavior;

		private bool isInside = false;
		public bool isOutside = true;



        [Header("Audio")]
        public AudioSource[] aSources;
        float audioTransition = 2f;
        bool playOnce = true;

		void Awake(){
			_instance = this;
		}

		void Start(){
			OutsidePortal ();
		}

		void OnTriggerEnter(Collider col){
            if(col.tag == "MainCamera")
            {
                Debug.Log("Camera Enter");
            }


			if (isOutside) {
				//animBehavior.PlayAnim ();
			}
		}

        void OnTriggerExit(Collider col)
        {
            if (col.tag == "MainCamera")
            {
                Debug.Log("Camera Exit");
            }
        }

        void OnTriggerStay(Collider col){
			Vector3 playerPos = Camera.main.transform.position + Camera.main.transform.forward * (Camera.main.nearClipPlane * 4);
			if (transform.InverseTransformPoint (playerPos).z <= 0) {
				if (isOutside) {
					isOutside = false;
					isInside = true;
					InsidePortal ();
					//animBehavior.EnableSprite (false);
				}
			} else {
				if (isInside) {
					isInside = false;
					isOutside = true;
					OutsidePortal ();
					//animBehavior.EnableSprite (true);
				}
			}
		}


        [ContextMenu("Out Portal")]
        public void OutsidePortal(){

            Debug.Log("Went out Portal");

			StartCoroutine (DelayChangeMat2 (3));
            StartCoroutine(DelayChangeMatNot(2));
            StartCoroutine("FadeOutAudio");

            //animBehavior.EnableSprite (true);
        }
        [ContextMenu("In Portal")]
        public void InsidePortal(){

            Debug.Log("Went in Portal");

            StartCoroutine (DelayChangeMat2 (6  ));
            StartCoroutine(DelayChangeMatNot(2));
            StartCoroutine("FadeInAudio");
            //animBehavior.EnableSprite (false);
        }


        /*
		IEnumerator DelayChangeMat(int stencilNum){
			unityARVideo.shouldRender = false;
			yield return new WaitForEndOfFrame ();
			meshRenderer.enabled = false;
			foreach (var mat in materials) {
				mat.SetInt ("_StencilTest", stencilNum);
			}
			yield return new WaitForEndOfFrame ();
			meshRenderer.enabled = true;
			unityARVideo.shouldRender = true;
		}*/

        IEnumerator DelayChangeMat2(int stencilNum)
        {
            //unityARVideo.shouldRender = false;
            //yield return new WaitForEndOfFrame();
            meshRenderer.enabled = false;
            foreach (var mat in materials)
            {
                mat.SetInt("_StencilTest", stencilNum);
                mat.SetInt("_StencilComp", stencilNum);
            }
            yield return new WaitForEndOfFrame();
            meshRenderer.enabled = true;
            //unityARVideo.shouldRender = true;
        }

        IEnumerator DelayChangeMatNot(int stencilNum)
        {
            //unityARVideo.shouldRender = false;
            //yield return new WaitForEndOfFrame();
            meshRenderer.enabled = false;

                materialFloor.SetInt("_StencilTest2", 2);

            yield return new WaitForEndOfFrame();
            meshRenderer.enabled = true;
            //unityARVideo.shouldRender = true;
        }

        IEnumerator FadeInAudio()
        {
            float duration = audioTransition;

            if (playOnce) { 
                foreach (var item in aSources)
                {
                    //if(!item.isPlaying)
                    item.Play();
                }
                playOnce = false;
            }
            else
            {
                foreach (var item in aSources)
                {
                    //if(!item.isPlaying)
                    item.UnPause();
                }
            }

            for (float i = 0; i <= 1; i+= Time.deltaTime / duration)
            {
                foreach (var item in aSources)
                {
                    item.volume = i;
                }
                yield return null;
            }
            foreach (var item in aSources)
            {
                item.volume = 1;
            }
        }

        IEnumerator FadeOutAudio()
        {
            float duration = audioTransition;

            for (float i = 1; i > 0; i -= Time.deltaTime / duration)
            {
                foreach (var item in aSources)
                {
                    item.volume = i;
                }
                yield return null;
            }
            foreach (var item in aSources)
            {
                item.volume = 0;
                item.Pause();
            }
        }
    }
}


