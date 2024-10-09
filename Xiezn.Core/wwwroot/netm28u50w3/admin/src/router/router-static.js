import Vue from 'vue';
//配置路由
import VueRouter from 'vue-router'
Vue.use(VueRouter);
//1.创建组件
import Index from '@/views/index'
import Home from '@/views/home'
import Login from '@/views/login'
import NotFound from '@/views/404'
import UpdatePassword from '@/views/update-password'
import pay from '@/views/pay'
import register from '@/views/register'
import center from '@/views/center'
    import rukuxinxi from '@/views/modules/rukuxinxi/list'
    import gongyingshang from '@/views/modules/gongyingshang/list'
    import shangpinkucun from '@/views/modules/shangpinkucun/list'
    import xiaoshouyuan from '@/views/modules/xiaoshouyuan/list'
    import xiaoshoudingdan from '@/views/modules/xiaoshoudingdan/list'
    import tuihuoxinxi from '@/views/modules/tuihuoxinxi/list'
    import shangpinleibie from '@/views/modules/shangpinleibie/list'
    import caigouyuan from '@/views/modules/caigouyuan/list'


//2.配置路由   注意：名字
export const routes = [{
    path: '/',
    name: '系统首页',
    component: Index,
    children: [{
      // 这里不设置值，是把main作为默认页面
      path: '/',
      name: '系统首页',
      component: Home,
      meta: {icon:'', title:'center', affix: true}
    }, {
      path: '/updatePassword',
      name: '修改密码',
      component: UpdatePassword,
      meta: {icon:'', title:'updatePassword'}
    }, {
      path: '/pay',
      name: '支付',
      component: pay,
      meta: {icon:'', title:'pay'}
    }, {
      path: '/center',
      name: '个人信息',
      component: center,
      meta: {icon:'', title:'center'}
    }
      ,{
	path: '/rukuxinxi',
        name: '入库信息',
        component: rukuxinxi
      }
      ,{
	path: '/gongyingshang',
        name: '供应商',
        component: gongyingshang
      }
      ,{
	path: '/shangpinkucun',
        name: '商品库存',
        component: shangpinkucun
      }
      ,{
	path: '/xiaoshouyuan',
        name: '销售员',
        component: xiaoshouyuan
      }
      ,{
	path: '/xiaoshoudingdan',
        name: '销售订单',
        component: xiaoshoudingdan
      }
      ,{
	path: '/tuihuoxinxi',
        name: '退货信息',
        component: tuihuoxinxi
      }
      ,{
	path: '/shangpinleibie',
        name: '商品类别',
        component: shangpinleibie
      }
      ,{
	path: '/caigouyuan',
        name: '采购员',
        component: caigouyuan
      }
    ]
  },
  {
    path: '/login',
    name: 'login',
    component: Login,
    meta: {icon:'', title:'login'}
  },
  {
    path: '/register',
    name: 'register',
    component: register,
    meta: {icon:'', title:'register'}
  },
  {
    path: '*',
    component: NotFound
  }
]
//3.实例化VueRouter  注意：名字
const router = new VueRouter({
  mode: 'hash',
  /*hash模式改为history*/
  routes // （缩写）相当于 routes: routes
})
const originalPush = VueRouter.prototype.push
//修改原型对象中的push方法
VueRouter.prototype.push = function push(location) {
   return originalPush.call(this, location).catch(err => err)
}
export default router;
