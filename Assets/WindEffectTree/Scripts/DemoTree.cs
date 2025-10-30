namespace Oicaimang.WindEffectTree
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DemoTree : MonoBehaviour
    {
        [SerializeField] List<GameObject> groupTrees;
        [SerializeField] float timeLoop;
        private void Start()
        {
            StartCoroutine(LoopShowTree(groupTrees.Count - 1));
        }
        IEnumerator LoopShowTree(int index)
        {
            yield return new WaitForSeconds(timeLoop);
            foreach (var tree in groupTrees)
            {
                tree.SetActive(false);
            }
            groupTrees[index].SetActive(true);
            index--;
            if (index == 0) index = groupTrees.Count - 1;
            StartCoroutine(LoopShowTree(index));
        }
    }
}
