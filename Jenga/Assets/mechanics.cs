using UnityEngine;
using System.Collections;
/*
public class mechanics : MonoBehaviour {

	void UpdatePinch(Frame frame) {
		bool trigger_pinch = false;
		Hand hand = frame.Hands[handIndex];
		
		// Thumb tip is the pinch position.
		Vector3 thumb_tip = hand.Fingers[0].TipPosition.ToUnityScaled();
		
		// Check thumb tip distance to joints on all other fingers.
		// If it's close enough, start pinching.
		for (int i = 1; i < NUM_FINGERS && !trigger_pinch; ++i) {
			Finger finger = hand.Fingers[i];
			
			for (int j = 0; j < NUM_JOINTS && !trigger_pinch; ++j) {
				Vector3 joint_position = finger.JointPosition((Finger.FingerJoint)(j)).ToUnityScaled();
				Vector3 distance = thumb_tip - joint_position;
				if (distance.magnitude < THUMB_TRIGGER_DISTANCE)
					trigger_pinch = true;
			}
		}
		
		// Only change state if it's different.
		if (trigger_pinch && !pinching_)
			OnPinch(pinch_position);
	}

	void OnPinch(Vector3 pinch_position) {
		// ...
		// Check if we pinched a movable object and grab the closest one.
		Collider[] close_things = Physics.OverlapSphere(pinch_position, PINCH_DISTANCE, layer_mask_);
		Vector3 distance = new Vector3(PINCH_DISTANCE, 0.0f, 0.0f);
		for (int j = 0; j < close_things.Length; ++j) {
			Vector3 new_distance = pinch_position - close_things[j].transform.position;
			if (close_things[j].rigidbody != null && new_distance.magnitude < distance.magnitude) {
				grabbed_object_ = close_things[j];
				distance = new_distance;
			}
		}
	}
	
	// Update is called once per frame
	void Update() {
		// ...
		// Accelerate what we are grabbing toward the pinch.
		if (grabbed_ != null) {
			Vector3 distance = pinch_position - grabbed_.transform.position;
			grabbed_object_.rigidbody.AddForce(SPRING_CONSTANT * distance);
		}
	}
}
*/