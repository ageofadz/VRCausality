using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverfix : MonoBehaviour
{
    private Quaternion initialLocalRotation;
    private Vector3 initialLocalPosition;

    private Quaternion localRotationOnDisable;
    private Vector3 localPositionOnDisable;

    private bool hasDisabled;

    public void Awake()
    {
        this.initialLocalRotation = this.transform.localRotation;
        this.initialLocalPosition = this.transform.localPosition;
    }

    void OnDisable()
    {
        this.localRotationOnDisable = this.transform.localRotation;
        this.transform.localRotation = this.initialLocalRotation;

        this.localPositionOnDisable = this.transform.localPosition;
        this.transform.localPosition = this.initialLocalPosition;

        this.hasDisabled = true;
    }

    void Update()
    {
        if (this.hasDisabled)
        {
            this.hasDisabled = false;
            this.transform.localRotation = this.localRotationOnDisable;
            this.transform.localPosition = this.localPositionOnDisable;
        }
    }
}
