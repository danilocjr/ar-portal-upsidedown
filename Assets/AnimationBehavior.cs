using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.iOS
{
	public class AnimationBehavior : MonoBehaviour {

		private Animation anim;
		private SpriteRenderer sp;
		public PortalController portalController;
		private Vector3 startAngle;
		private bool animHasplayed = false;

		void Start(){
			anim = GetComponent<Animation> ();
			sp = GetComponent<SpriteRenderer> ();
			startAngle = transform.localEulerAngles;
		}
		public void PlayAnim(){
			if (!animHasplayed){
				animHasplayed = true;
				anim.Play ();
			}
		}

		public void EnableSprite(bool enable){
			if (enable) {
				StartCoroutine (DelayEnable ());
			} else {
				sp.enabled = false;
			}
		}

		IEnumerator DelayEnable(){
			yield return new WaitForSeconds (3f);
			sp.enabled = true;
			animHasplayed = false;
			transform.localEulerAngles = startAngle;
		}
	}
}
