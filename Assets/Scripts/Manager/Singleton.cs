using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 제네릭 클래스 형식의 싱글턴,
/// 해당 싱글턴을 상속받고 instance의 Type을 쓰고자 하는 형식의 Type의 instance로 생성,
/// 단 Monobehaviour을 무조건 상속받게 됨.
/// </summary>
/// <typeparam name="T">싱글턴으로 만들고싶은 인스턴스의 타입 입력.</typeparam>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;

                if (instance == null)
                {
                    Debug.LogError("해당하는 " + typeof(T) + "타입의 클래스가 해당 씬에 존재하지 않음");
                }
            }
            return instance;
        }
    }

}