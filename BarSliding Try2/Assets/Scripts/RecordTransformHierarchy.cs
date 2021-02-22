using UnityEngine;
using UnityEditor.Animations;

public class RecordTransformHierarchy : MonoBehaviour
{
    public AnimationClip clip;
    public bool record = false;

    private GameObjectRecorder m_Recorder;

    void Start()
    {
        // Create recorder and record the script GameObject.
        m_Recorder = new GameObjectRecorder(gameObject);

        // Bind all the Transforms on the GameObject and all its children.
        m_Recorder.BindComponentsOfType<Transform>(gameObject, true);
    }

    void LateUpdate()
    {
        if (clip == null)
            return;

        // Take a snapshot and record all the bindings values for this frame.
        m_Recorder.TakeSnapshot(Time.deltaTime);
    }

    void OnDisable()
    {
        if (clip == null)
            return;

        if (record)
        {
            // As long as "record" is on: take a snapshot.
            m_Recorder.TakeSnapshot(Time.deltaTime);
        }
        else if (m_Recorder.isRecording)
        {
            // "record" is off, but we were recording:
            // save to clip and clear recording.
            m_Recorder.SaveToClip(clip);
            m_Recorder.ResetRecording();
        }
    }
}