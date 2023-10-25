using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class GameConstants
{
    public static Vector3 CameraAngleOffset { get; set; }
    public static Vector3 CameraPositionOffset { get; set; }

    public static float Damping { get; set; }
}
public class ThirdPersonCamera : MonoBehaviour
{

    public Vector3 mAngleOffset = new Vector3(0.0f, 0.0f, 0.0f);

    public float mDamping = 1.0f;

    public Vector3 mPositionOffset = new Vector3(0.0f, 2.0f, -2.5f);


    public abstract class TPCBase
    {
        protected Transform mCameraTransform;
        protected Transform mPlayerTransform;
        public Transform CameraTransform
        {
            get
            {
                return mCameraTransform;
            }
        }
        public Transform PlayerTransform
        {
            get
            {
                return mPlayerTransform;
            }
        }
        public TPCBase(Transform cameraTransform, Transform playerTransform)
        {
            mCameraTransform = cameraTransform;
            mPlayerTransform = playerTransform;
        }
        public abstract void Update();
    }

    public class TPCTrack : TPCBase
    {
        public TPCTrack(Transform cameraTransform, Transform playerTransform)
: base(cameraTransform, playerTransform)
        {
        }
        public override void Update()
        {
            Vector3 targetPos = mPlayerTransform.position;

            targetPos.y += GameConstants.CameraPositionOffset.y;
            mCameraTransform.LookAt(targetPos);
        }
    }

    public Transform mPlayer;
    TPCBase mThirdPersonCamera;

    private void Start()
    {
        mThirdPersonCamera = new TPCTrack(transform, mPlayer);

        GameConstants.Damping = mDamping;
        GameConstants.CameraPositionOffset = mPositionOffset;
        GameConstants.CameraAngleOffset = mAngleOffset;

        GameConstants.CameraPositionOffset = mPositionOffset;
        //mThirdPersonCamera = new TPCTrack(transform, mPlayer);
        //mThirdPersonCamera = new TPCFollowTrackPosition(transform, mPlayer);

        mThirdPersonCamera = new TPCFollowTrackPositionAndRotation(transform, mPlayer);
    }

    private void LateUpdate()
    {
        mThirdPersonCamera.Update();
    }

    public abstract class TPCFollow : TPCBase
    {
        public TPCFollow(Transform cameraTransform, Transform PlayerTransform) : base(cameraTransform, PlayerTransform)
        {

        }

        public override void Update()
        {
            Vector3 forward = mPlayerTransform.forward;
            Vector3 right = mPlayerTransform.right;
            Vector3 up = mPlayerTransform.up;

            Vector3 targetPos = mPlayerTransform.position;

            Vector3 desiredPosition = targetPos + GameConstants.CameraPositionOffset;

            Vector3 position = Vector3.Lerp(mCameraTransform.position, desiredPosition, Time.deltaTime * GameConstants.Damping);
            mCameraTransform.position = position;
        }
    }

    public class TPCFollowTrackPosition : TPCFollow
    {
        public TPCFollowTrackPosition(Transform cameraTransform, Transform
        playerTransform)
        : base(cameraTransform, playerTransform)
        {
        }
        public override void Update()
        {
            
            Quaternion initialRotation =
            Quaternion.Euler(GameConstants.CameraAngleOffset);
            
            mCameraTransform.rotation =
            Quaternion.RotateTowards(mCameraTransform.rotation,
            initialRotation,
            Time.deltaTime * GameConstants.Damping);
            
            base.Update();
        }
    }

    public class TPCFollowTrackPositionAndRotation : TPCFollow
    {
        public TPCFollowTrackPositionAndRotation(Transform cameraTransform, Transform playerTransform): base(cameraTransform, playerTransform)
        {

        }

        public override void Update()
        {
            Quaternion initalRotation = Quaternion.Euler(GameConstants.CameraAngleOffset);

            mCameraTransform.rotation = Quaternion.Lerp(mCameraTransform.rotation, mPlayerTransform.rotation * initalRotation, Time.deltaTime * GameConstants.Damping);

            base.Update();
        }
    }


    
}
