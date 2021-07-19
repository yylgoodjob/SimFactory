using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimFactory
{

    public class GameManager : MonoBehaviour
    {
        //当前鼠标附着事件
        public MouseType.mouseType mouse;
        //当指针物体
        public GameObject pointer;
        //地图长宽
        public int MapLong;
        public int MapWide;

        private float sinTime;
        private float gameTime;

        private bool taskBase;//任务器
        

        /// <summary>
        /// 该数组记录所有生成的土地
        /// </summary>
        public List<List<GameObject>> Lands = new List<List<GameObject>>();

        public List<List<float>> GrowPlant = new List<List<float>>();

        private void Awake()
        {
            //添加事件
            EventCenter.AddListener<GameObject, GameObject, Vector3, float>(EventType.CREATELANDS, Create_Lands);
            EventCenter.AddListener(EventType.RANDOMMAP, RandomMap);
            EventCenter.AddListener<int, int>(EventType.CHANGEMAPSIZE, ChangeMapSize);
            EventCenter.AddListener<bool>(EventType.ISJOBFACTROY, FindisCanJob);
            EventCenter.AddListener<bool>(EventType.TASKBASE, UpdateTaskBase);
        }
        public void Start()
        {
            mouse = MouseType.mouseType.EMPTY;//初始化默认为空事件
            sinTime = 0;
            gameTime = 0;
            taskBase = true;//任务器默认打开
        }
        private void Update()
        {
            FollowMouse();
            LeaveMouse();
            ChangeScreenSize();
            ChangeScreenPos(0.05f * Mathf.Abs(GameObject.FindGameObjectWithTag("MainCamera").transform.position.z));
        }

        private void FixedUpdate()
        {
            if (Managers.m_Managers.m_ScenesManager.Order == 1)
            {
                sinTime += Time.deltaTime;
                
                if (sinTime > 10.0f)
                {
                    //污水处理
                    GoSewageTreat();
                    sinTime = 0;
                }
                //作物生长的方法
                PlantGrow();
                
                //测试(需要一个开关)
                if(taskBase)//当任务器打开是才能检测任务
                {
                    gameTime += Time.deltaTime;
                    if (gameTime > 10.0f)
                    {
                        //Debug.Log("更新任务");
                        UpdateTask();
                        gameTime = 0;
                    }
                }
            }
            else
            {
                sinTime = 0;
            }
            
        }

        private void OnDestroy()
        {
            
            EventCenter.RemoveListener<GameObject, GameObject, Vector3, float>(EventType.CREATELANDS, Create_Lands);
            EventCenter.RemoveListener(EventType.RANDOMMAP, RandomMap);
            EventCenter.RemoveListener<int, int>(EventType.CHANGEMAPSIZE, ChangeMapSize);
            EventCenter.RemoveListener<bool>(EventType.ISJOBFACTROY, FindisCanJob);
            EventCenter.RemoveListener<bool>(EventType.TASKBASE, UpdateTaskBase);
        }

        /// <summary>
        /// 创建go物体
        /// </summary>
        /// <param name="parent">go物体的父物体</param>
        /// <param name="go"></param>
        /// <param name="Pos">go物体的初始化位置</param>
        /// <param name="dis">每两个挨着的go物体的距离</param>
        private void Create_Lands(GameObject parent,GameObject go, Vector3 Pos, float dis)
        {
            //清空数据，确保初始化为空开始
            GrowPlant = null;
            GrowPlant = new List<List<float>>();

            Vector3 pos = Pos;
            for(int i = 0; i < MapLong; i++)
            {
                for(int j = 0; j < MapWide; j++)
                {
                    InitLand(parent, go, pos, i, j);
                    pos.y -= dis;
                }
                pos.x -= dis;
                pos.y = Pos.y;
            }
        }

        /// <summary>
        /// 更新地图长度
        /// </summary>
        /// <param name="l">长</param>
        /// <param name="w">宽</param>
        private void ChangeMapSize(int l, int w)
        {
            this.MapLong = l;
            this.MapWide = w;
        }

        /// <summary>
        /// 随机地图
        /// </summary>
        private void RandomMap()
        {
            Vector2 pos = new Vector2(0, 0);
            //仓库1
            pos = RandomPos2(pos);
            Lands[(int)pos.x][(int)pos.y].GetComponent<Land>().landType = LandType.LandsType.EXPORT;
            Lands[(int)pos.x][(int)pos.y].GetComponent<Land>().ChangeLand();
            //仓库2
            pos = RandomPos2(pos);
            Lands[(int)pos.x][(int)pos.y].GetComponent<Land>().landType = LandType.LandsType.ENTRANCE;
            Lands[(int)pos.x][(int)pos.y].GetComponent<Land>().ChangeLand();
            //购入点
            pos = RandomPos2(pos);
            Lands[(int)pos.x][(int)pos.y].GetComponent<Land>().landType = LandType.LandsType.EMPLOYHOME;
            Lands[(int)pos.x][(int)pos.y].GetComponent<Land>().ChangeLand();
        }
        /// <summary>
        /// 生成随机位置
        /// </summary>
        /// <returns></returns>
        private Vector2 RandomPos2(Vector2 pos)
        {
            int x = Random.Range(0, MapLong - 1);
            int y = Random.Range(0, MapWide - 1);
            if((int)pos.x == x && (int)pos.y == y)
            {
                return RandomPos2(pos);
            }
            return new Vector2(x, y);
        }

        public void InitLand(GameObject parent, GameObject go, Vector3 Pos, int i, int j) 
        {
            GameObject land = Instantiate(go);
            //设置位置
            land.transform.position = Pos;
            //设置标签
            land.GetComponent<Land>().landType = LandType.LandsType.EMPTY;//默认为空地
            //设置父物体
            land.transform.SetParent(parent.transform);
            //添加到Lands中
            if(i >= Lands.Count)
            {
                Lands.Add(new List<GameObject>());
                GrowPlant.Add(new List<float>());
            }
            Lands[Lands.Count - 1].Add(land);
            GrowPlant[Lands.Count - 1].Add(0.0f);
        }
        /// <summary>
        /// 物体跟随鼠标的方法
        /// </summary>
        /// <param name="go"></param>
        private void FollowMouse()
        {
            if(this.pointer != null)
            {
                pointer.transform.position = Input.mousePosition;
            }
        }


        /// <summary>
        /// 物体离开鼠标的方法
        /// </summary>
        private void LeaveMouse()
        {
            if (Input.GetMouseButton(1))//点击右键
            {
                if (this.pointer != null && this.mouse != MouseType.mouseType.EMPTY)
                {
                    if (this.mouse == MouseType.mouseType.INTO)
                    {
                        EventCenter.Broadcast<int>(EventType.CHANGEGOLD, GameObject.FindGameObjectWithTag("DesPanel").GetComponent<DesPanel>().RawPrice[pointer.GetComponent<Lat>().Raw]);
                    }
                    Destroy(this.pointer);
                    this.pointer = null;
                    this.mouse = MouseType.mouseType.EMPTY;
                }
            }
        }

        /// <summary>
        /// 变换视角大小
        /// </summary>
        public void ChangeScreenSize()
        {
            if(Managers.m_Managers.m_ScenesManager.Order == 1)//当前为第一场景
            {
                GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
                if (Input.GetAxis("Mouse ScrollWheel") > 0)
                {
                    if(go.transform.position.z < -2.0f)
                    {
                        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z + 1);
                    }
                    
                }else if(Input.GetAxis("Mouse ScrollWheel") < 0)
                {
                    if(go.transform.position.z > -10.0f)
                    {
                        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, go.transform.position.z - 1);
                    }
                    
                }
            }
        }

        /// <summary>
        /// 改变视角位置
        /// </summary>
        public void ChangeScreenPos(float dis)
        {
            if(Managers.m_Managers.m_ScenesManager.Order == 1)//当前场景为第一场景
            {
                GameObject go = GameObject.FindGameObjectWithTag("MainCamera");
                if (Input.GetKey("w"))//12
                {
                    if(go.transform.position.y > 12)
                    {
                        return;
                    }
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + dis, go.transform.position.z);
                }else if(Input.GetKey("a"))//-23
                {
                    if (go.transform.position.x < -23)
                    {
                        return;
                    }
                    go.transform.position = new Vector3(go.transform.position.x - dis, go.transform.position.y, go.transform.position.z);
                }
                else if(Input.GetKey("s"))//-12
                {
                    if (go.transform.position.y < -12)
                    {
                        return;
                    }
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y - dis, go.transform.position.z);
                }
                else if(Input.GetKey("d"))//23
                {
                    if (go.transform.position.x > 23)
                    {
                        return;
                    }
                    go.transform.position = new Vector3(go.transform.position.x + dis, go.transform.position.y, go.transform.position.z);
                }
            }
        }

        /// <summary>
        /// 查找不能工作的工厂是否能进行工作
        /// </summary>
        private void FindisCanJob(bool isBuild)
        {
            //需要搜索的物体位置
            List<List<int> > list = new List<List<int> >();
            //搜索地图
            List<List<int> > Map = new List<List<int> >();

            //获取地图信息
            for(int i = 0; i < this.Lands.Count; i++)
            {
                Map.Add(new List<int>());
                for(int j = 0; j < this.Lands[i].Count; j++)
                {
                    LandType.LandsType type = Lands[i][j].GetComponent<Land>().landType;
                    //当判定为建筑，并且此时不能工作时，加入搜索行列，确定该工厂能否正常工作
                    if (isBuild && type != LandType.LandsType.EMPTY && type != LandType.LandsType.OB5 && Lands[i][j].GetComponent<Land>().work == LandType.IsJob.NO)
                    {
                        if (type != LandType.LandsType.OB5 && type != LandType.LandsType.OB6 && type != LandType.LandsType.PLANT1 && type != LandType.LandsType.PLANT2 && type != LandType.LandsType.PLANT3 && type != LandType.LandsType.PLANT4 && type != LandType.LandsType.PLANT5)
                            list.Add(new List<int>() { i, j}); 
                    }else if(!isBuild && type != LandType.LandsType.EMPTY && type != LandType.LandsType.OB5 && type != LandType.LandsType.OB6)
                    {
                        if (type != LandType.LandsType.OB5 && type != LandType.LandsType.OB6 && type != LandType.LandsType.PLANT1 && type != LandType.LandsType.PLANT2 && type != LandType.LandsType.PLANT3 && type != LandType.LandsType.PLANT4 && type != LandType.LandsType.PLANT5)
                            list.Add(new List<int>() { i, j });
                    }
                    Map[i].Add((int)Lands[i][j].GetComponent<Land>().landType);
                }
            }
            
            //Debug.Log(list.Count);
            /*for(int i = 0; i < Map.Count; i++)
            {
                Debug.Log(Map[i][0] + " " + Map[i][1] + " " + Map[i][2] + " " + Map[i][3] + " " + Map[i][4]);
            }
            for(int i = 0; i < list.Count; i++)
            {
                Debug.Log(list[i][0] + " " + list[i][1]);
            }*/

            //搜索
            for(int i = 0; i < list.Count; i++)
            {
                /*if (DFS_isJob(Map, list[i][0], list[i][1], 8) && DFS_isJob(Map, list[i][0], list[i][1], 9) && DFS_isJob(Map, list[i][0], list[i][1], 10))
                {
                    Debug.Log(Lands[list[i][0]][list[i][1]].GetComponent<Land>().landType + "可以工作");
                }*/
                List<List<int> > m1 = new List<List<int> >();
                List<List<int> > m2 = new List<List<int> >();
                List<List<int> > m3 = new List<List<int> >();
                for(int j = 0; j < Map.Count; j++)
                {
                    m1.Add(new List<int>());
                    m2.Add(new List<int>());
                    m3.Add(new List<int>());
                    for(int k = 0; k < Map[j].Count; k++)
                    {
                        m1[j].Add(Map[j][k]);
                        m2[j].Add(Map[j][k]);
                        m3[j].Add(Map[j][k]);
                    }
                }
                if(DFS_isJob(m1, list[i][0], list[i][1], 8) && DFS_isJob(m2, list[i][0], list[i][1], 9) && DFS_isJob(m3, list[i][0], list[i][1], 10))
                {
                    //Debug.Log(Lands[list[i][0]][list[i][1]].GetComponent<Land>().landType + "可以工作");
                    //标记为可工作
                    Lands[list[i][0]][list[i][1]].GetComponent<Land>().work = LandType.IsJob.YES;
                    //将该工厂挂载仓库脚本
                    //Lands[list[i][0]][list[i][1]].AddComponent<WareHouse>();
                    //添加仓库标签
                    //Lands[list[i][0]][list[i][1]].GetComponent<Land>().AddFactLabel();
                }
                else
                {
                    //标记为不可工作
                    Lands[list[i][0]][list[i][1]].GetComponent<Land>().work = LandType.IsJob.NO;
                }
                
            }
        }

        /// <summary>
        /// dfs搜索
        /// </summary>
        /// <param name="Map">地图</param>
        /// <param name="x">当前位置x</param>
        /// <param name="y">当前位置y</param>
        /// <param name="type">要查找的标签</param>
        /// <returns></returns>
        private bool DFS_isJob(List<List<int> > Map, int x, int y, int Goal)
        {
            if(Map[x][y] == Goal)
            {
                return true;
            }
            Map[x][y] = 0;//重置为空地

            //搜索四个方向
            if(x - 1 >= 0 && Map[x - 1][y] != 0)
            {
                if(DFS_isJob(Map, x - 1, y, Goal))
                {
                    return true;
                }
            }
            if(x + 1 < Map.Count && Map[x + 1][y] != 0)
            {
                if(DFS_isJob(Map, x + 1, y, Goal))
                {
                    return true;
                }
            }
            if(y - 1 >= 0 && Map[x][y - 1] != 0)
            {
                if(DFS_isJob(Map, x, y - 1, Goal))
                {
                    return true;
                }
            }
            if (y + 1 < Map[x].Count && Map[x][y + 1] != 0)
            {
                if(DFS_isJob(Map, x, y + 1, Goal))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 污水(bfs)
        /// </summary>
        private void GoSewageTreat()
        {
            for(int i = 0; i < Lands.Count; i++)
            {
                for(int j = 0; j < Lands[i].Count; j++)
                {
                    SewageTreat sewage = Lands[i][j].GetComponent<SewageTreat>();
                    //处理污水
                    if (Lands[i][j].GetComponent<Land>().landType == LandType.LandsType.OB4)
                    {
                        sewage.ReduceSewage(5);
                    }
                    SewageTreat left_sewage = null;
                    if(j - 1 >= 0)
                    left_sewage = Lands[i][j - 1].GetComponent<SewageTreat>();
                    SewageTreat right_sewage = null;
                    if(j + 1 < Lands[i].Count)
                    right_sewage = Lands[i][j + 1].GetComponent<SewageTreat>();
                    SewageTreat up_sewage = null;
                    if(i - 1 >= 0)
                    up_sewage = Lands[i - 1][j].GetComponent<SewageTreat>();
                    SewageTreat down_sewage = null;
                    if(i + 1 < Lands.Count)
                    down_sewage = Lands[i + 1][j].GetComponent<SewageTreat>();
                    if(sewage != null)
                    {
                        if(left_sewage != null)
                        {
                            if (left_sewage.Capacity > 1)
                            {
                                sewage.UniformSewage(left_sewage.gameObject);
                            }else
                            {
                                left_sewage.Capacity = 0;
                            }
                        }
                        if (right_sewage != null)
                        {
                            if (right_sewage.Capacity > 1)
                            {
                                sewage.UniformSewage(right_sewage.gameObject);
                            }else
                            {
                                right_sewage.Capacity = 0;
                            }
                        }
                        if (up_sewage != null)
                        {
                            if (up_sewage.Capacity > 1)
                            {
                                sewage.UniformSewage(up_sewage.gameObject);
                            }else
                            {
                                up_sewage.Capacity = 0;
                            }
                        }
                        if (down_sewage != null)
                        {
                            if (down_sewage.Capacity > 1)
                            {
                                sewage.UniformSewage(down_sewage.gameObject);
                            }else
                            {
                                down_sewage.Capacity = 0;
                            }
                        }
                        if(sewage.Capacity > 300)//后期调整数值
                        {
                            EventCenter.Broadcast(EventType.CHANGEGOLD, -1);
                        }
                    }
                    
                }
            }
        }

        /// <summary>
        /// 植物生长的方法
        /// </summary>
        private void PlantGrow()
        {
            //调试(原因：Lands的大小钰GrowPlant的大小不一致，下标不同)
            /*Debug.Log("GrowPlant.Count = " + GrowPlant.Count);
            Debug.Log("GrowPlant[i].Count = " + GrowPlant[0].Count);
            Debug.Log("Lands.Count = " + Lands.Count);
            Debug.Log("Lands[i].Count = " + Lands[0].Count);*/
            for (int i = 0; i < GrowPlant.Count; i++)
            {
                for(int j = 0; j < GrowPlant[i].Count; j++)
                {
                    LandType.LandsType type = Lands[i][j].GetComponent<Land>().landType;
                    //当该块地为植物时
                    if(type == LandType.LandsType.PLANT1 || Lands[i][j].GetComponent<Land>().landType == LandType.LandsType.PLANT2 || Lands[i][j].GetComponent<Land>().landType == LandType.LandsType.PLANT3 || Lands[i][j].GetComponent<Land>().landType == LandType.LandsType.PLANT4 || Lands[i][j].GetComponent<Land>().landType == LandType.LandsType.PLANT5)
                    {
                        if(Lands[i][j].GetComponent<Land>().work == LandType.IsJob.NO)//增长未成熟作物的时间
                        GrowPlant[i][j] += Time.deltaTime;
                    }
                    
                    //判断植物是否成熟
                    if(IsMaturePlant(type, GrowPlant[i][j]))
                    {
                        //成熟(标记为可以收获)
                        Lands[i][j].GetComponent<Land>().work = LandType.IsJob.YES;
                        GrowPlant[i][j] = 0;
                    }
                }
            }
        }
        /// <summary>
        /// 判断植物是否成熟
        /// </summary>
        /// <param name="type">植物类型</param>
        /// <param name="time">当前度过时间</param>
        /// <returns>是否成熟</returns>
        private bool IsMaturePlant(LandType.LandsType type, float time) 
        {
            switch(type)
            {
                case LandType.LandsType.PLANT1:
                    if(time > 20.0f)//成熟
                    {
                        return true;
                    }
                    break;
                case LandType.LandsType.PLANT2:
                    if (time > 20.0f)//成熟
                    {
                        return true;
                    }
                    break;
                case LandType.LandsType.PLANT3:
                    if (time > 20.0f)//成熟
                    {
                        return true;
                    }
                    break;
                case LandType.LandsType.PLANT4:
                    if (time > 20.0f)//成熟
                    {
                        return true;
                    }
                    break;
                case LandType.LandsType.PLANT5:
                    if (time > 20.0f)//成熟
                    {
                        return true;
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// 更新任务事件
        /// </summary>
        private void UpdateTask()
        {
            TaskType taskType = GameObject.FindGameObjectWithTag("TaskBar").GetComponent<TaskType>();
            Debug.Log(Random.Range(0, taskType.TaskClass()));
            EventCenter.Broadcast<TaskType.TASKTYPE>(EventType.ADDTASK, (TaskType.TASKTYPE)Random.Range(0, taskType.TaskClass()));
        }

        /// <summary>
        /// 调整任务器
        /// </summary>
        private void UpdateTaskBase(bool pand)
        {
            taskBase = pand;
        }
    }
}
