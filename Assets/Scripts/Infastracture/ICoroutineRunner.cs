using System.Collections;
using UnityEngine;

namespace Infastracture
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}