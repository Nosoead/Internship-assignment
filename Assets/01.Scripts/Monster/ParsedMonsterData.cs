using System;

[Serializable]
public class ParsedMonsterData
{
    public string monsterID;
    public string name;
    public string description;
    public int attack;
    public float attackMul;
    public int maxHP;
    public float maxHPMul;
    public int attackRange;
    public float attackRangeMul;
    public float attackSpeed;
    public float moveSpeed;
    public int minExp;
    public int maxExp;
    public int[] dropItem; //Git으로 받은 패키지로 int[] 배열로 받는 방안을 찾지 못함. DB로 변환할때 int[]로 바꿀 예정.
}
