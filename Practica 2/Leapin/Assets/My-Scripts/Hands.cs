using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap;
using Leap.Unity;
using Leap.Unity.RuntimeGizmos;


public class Hands : MonoBehaviour {
    
    private Hand hand;
    private Controller controller;
    /*
    [Tooltip("The palm of the hand.")]
    public Transform Palm;
    [Tooltip("The center of the forearm.")]
    public Transform Arm;
    [Tooltip("The tip of the thumb.")]
    public Transform Thumb;
    [Tooltip("The pont between the thumb and index finger.")]
    public Transform PinchPoint;
    [Tooltip("The tip of the index finger.")]
    public Transform Index;
    [Tooltip("The tip of the middle finger.")]
    public Transform Middle;
    [Tooltip("The tip of the ring finger.")]
    public Transform Ring;
    [Tooltip("The tip of the little finger.")]
    public Transform Pinky;
    [Tooltip("The point midway between the finger tips.")]
    public Transform GrabPoint;
    */
    // Use this for initialization
    void Start () {
        controller = new Controller();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Frame frame = controller.Frame();
        Vector3 pitch_yaw_roll = new Vector3();
        if (frame.Hands.Count > 0)
        {
            List<Hand> hands = frame.Hands;
            Hand hand = hands[0];
            Vector position = hand.PalmPosition;
            Vector velocity = hand.PalmVelocity;
            Vector direction = hand.Direction;
            Debug.Log("Velocidad de la palma:");
            Debug.Log(hand.PalmVelocity.ToVector3());
            Debug.Log("Normal de la palma:");
            Debug.Log(hand.Direction.ToVector3());
            Debug.Log("Pitch Yaw Roll:");
            pitch_yaw_roll.Set(hand.Direction.Pitch, hand.Direction.Yaw, hand.PalmNormal.Roll);
            //Si queremos mirar si está hacia arriba o hacia abajo, entonces miramos si roll es 0, abajo, si roll es 3 arriba
            Debug.Log(pitch_yaw_roll);
        }
        /*
        if (Palm != null)
        {
            Palm.position = _hand.PalmPosition.ToVector3();
            Palm.rotation = _hand.Basis.rotation.ToQuaternion();
        }
        if (Arm != null)
        {
            Arm.position = _hand.Arm.Center.ToVector3();
            Arm.rotation = _hand.Arm.Basis.rotation.ToQuaternion();
        }
        if (Thumb != null)
        {
            Thumb.position = _hand.Fingers[0].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint.ToVector3();
            Thumb.rotation = _hand.Fingers[0].Bone(Bone.BoneType.TYPE_DISTAL).Rotation.ToQuaternion();
        }
        if (Index != null)
        {
            Index.position = _hand.Fingers[1].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint.ToVector3();
            Index.rotation = _hand.Fingers[1].Bone(Bone.BoneType.TYPE_DISTAL).Rotation.ToQuaternion();
        }
        if (Middle != null)
        {
            Middle.position = _hand.Fingers[2].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint.ToVector3();
            Middle.rotation = _hand.Fingers[2].Bone(Bone.BoneType.TYPE_DISTAL).Rotation.ToQuaternion();
        }
        if (Ring != null)
        {
            Ring.position = _hand.Fingers[3].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint.ToVector3();
            Ring.rotation = _hand.Fingers[3].Bone(Bone.BoneType.TYPE_DISTAL).Rotation.ToQuaternion();
        }
        if (Pinky != null)
        {
            Pinky.position = _hand.Fingers[4].Bone(Bone.BoneType.TYPE_DISTAL).NextJoint.ToVector3();
            Pinky.rotation = _hand.Fingers[4].Bone(Bone.BoneType.TYPE_DISTAL).Rotation.ToQuaternion();
        }
        if (PinchPoint != null)
        {
            Vector thumbTip = _hand.Fingers[0].TipPosition;
            Vector indexTip = _hand.Fingers[1].TipPosition;
            Vector pinchPoint = Vector.Lerp(thumbTip, indexTip, 0.5f);
            PinchPoint.position = pinchPoint.ToVector3();

            Vector forward = pinchPoint - _hand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).PrevJoint;
            Vector up = _hand.Fingers[1].Bone(Bone.BoneType.TYPE_PROXIMAL).Direction.Cross(forward);
            PinchPoint.rotation = Quaternion.LookRotation(forward.ToVector3(), up.ToVector3());
        }
        if (GrabPoint != null)
        {
            var fingers = _hand.Fingers;
            Vector3 GrabCenter = _hand.WristPosition.ToVector3();
            Vector3 GrabForward = Vector3.zero;
            for (int i = 0; i < fingers.Count; i++)
            {
                Finger finger = fingers[i];
                GrabCenter += finger.TipPosition.ToVector3();
                if (i > 0)
                { //don't include thumb
                    GrabForward += finger.TipPosition.ToVector3();
                }
            }
            GrabPoint.position = GrabCenter / 6.0f; //average between wrist and fingertips
            GrabForward = (GrabForward / 4 - _hand.WristPosition.ToVector3()).normalized;
            Vector3 thumbToPinky = fingers[0].TipPosition.ToVector3() - fingers[4].TipPosition.ToVector3();
            Vector3 GrabNormal = Vector3.Cross(GrabForward, thumbToPinky).normalized;
            GrabPoint.rotation = Quaternion.LookRotation(GrabForward, GrabNormal);
        }
        */
    }

    /******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/



}
