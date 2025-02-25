export type SiteConfig = typeof siteConfig;

export const siteConfig = {
  name: "Dotnet Practice",
  description: "This is a sample client UI to connect to the dotnet backend",
  navItems: [
    {
      label: "Home",
      href: "/",
    },
    {
      label: "About",
      href: "/about",
    },
    {
      label: "Login",
      href: "/login",
    },
    {
      label: "Sign up",
      href: "/sign-up",
    },
  ],
  navMenuItems: [
    {
      label: "Home",
      href: "/",
    },
    {
      label: "About",
      href: "/about",
    },
    {
      label: "Login",
      href: "/login",
    },
    {
      label: "Sign up",
      href: "/sign-up",
    },
  ],
  links: {
    login: "/login",
    signup: "/sign-up",
  },
  requestUrls: {
    baseUrl: "https://localhost:7245",
    login: "/auth/login",
    signup: "/register-user",
    product: (id: string | null) => '/product' + (id ? `/${id}` : ""),
    products: '/user/products'
  },
};
