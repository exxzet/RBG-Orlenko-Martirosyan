using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float rotationSpeed = 100f;

    [Header("Combat")]
    public float physicalDamage = 10f;
    public float magicDamage = 20f;
    public float magicCooldown = 2f;
    public Transform attackPoint; 
    public float attackRange = 1f;
    public LayerMask enemyLayers; 

    [Header("Health")]
    public float maxHP = 100f;
    public float currentHP;

    private float nextMagicTime = 0f;
    private Rigidbody rb;
    private Animator animator;
    private bool isDead = false;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
      //animator = GetComponent<Animator>();
        currentHP = maxHP;
    }

    void Update()
    {
        if (isDead) return;


        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
        movement.Normalize();



        if (movement != Vector3.zero)
        {

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);



            rb.linearVelocity = transform.forward * movement.magnitude * (Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed);

        }
        else
        {
            rb.linearVelocity = Vector3.zero; 
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movement *= runSpeed;
         //   animator.SetBool("isRunning", true);
        }
        else
        {
            movement *= moveSpeed;
       //     animator.SetBool("isRunning", false);
        }

        rb.linearVelocity = movement;

        // �������� �������� (idle/walk/run) based on velocity
       // animator.SetFloat("Speed", rb.velocity.magnitude);


        // �����
        if (Input.GetMouseButtonDown(0))
        {
         //   animator.SetTrigger("Attack");
            PhysicalAttack();
        }

        if (Input.GetMouseButtonDown(1) && Time.time > nextMagicTime)
        {
            nextMagicTime = Time.time + magicCooldown;
        //    animator.SetTrigger("MagicAttack");
            MagicAttack();
        }
    }


    void PhysicalAttack()
    {
        // ������ ��������� ����������� �����
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider enemy in hitEnemies)
        {
           // enemy.GetComponent<EnemyController>()?.TakeDamage(physicalDamage);
        }
    }

    void MagicAttack()
    {
        // ������ ��������� ����������� ����� (������: �������� �������)
        // ... (������ �������� � ������� �������)
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        if (currentHP <= 0)
        {
            Die();
        }
    }


    void Die()
    {
        isDead = true;
      //  animator.SetTrigger("Die");
        rb.linearVelocity = Vector3.zero; // ���������� ��������
        GetComponent<Collider>().enabled = false; // ��������� ���������
        // ... (������ ����� ����/������)
    }


}
