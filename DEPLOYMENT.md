# Deployment Guide for Render

This guide will help you deploy the SampleAPI project to Render.

## Prerequisites

1. A [Render account](https://render.com) (free tier available)
2. Your project pushed to a Git repository (GitHub, GitLab, or Bitbucket)
3. The Dockerfile and render.yaml files in your repository

## Files Added for Deployment

The following files have been added to enable Render deployment:

- **Dockerfile** - Multi-stage Docker build for optimized .NET 8 deployment
- **.dockerignore** - Excludes unnecessary files from Docker image
- **render.yaml** - Infrastructure as Code configuration for Render
- **appsettings.Production.json** - Production environment settings
- **.env.example** - Example environment variables needed

## Deployment Steps

### Option 1: Deploy Using render.yaml (Recommended)

1. **Push your code to Git**
   ```bash
   git add .
   git commit -m "Add Render deployment configuration"
   git push origin master
   ```

2. **Create a new Web Service on Render**
   - Go to [Render Dashboard](https://dashboard.render.com/)
   - Click "New +" → "Blueprint"
   - Connect your repository
   - Render will automatically detect the `render.yaml` file

3. **Configure Environment Variables**
   In the Render dashboard, add the following environment variables:
   - `ApiSettings__ApiKey` - Your secure API key (generate a strong random string)
   - `ASPNETCORE_ENVIRONMENT` - Set to `Production`
   - `ASPNETCORE_URLS` - Set to `http://+:8080`

4. **Deploy**
   - Click "Apply" to create the service
   - Render will automatically build and deploy your application

### Option 2: Manual Deployment

1. **Push your code to Git**
   ```bash
   git add .
   git commit -m "Add Render deployment configuration"
   git push origin master
   ```

2. **Create a new Web Service**
   - Go to [Render Dashboard](https://dashboard.render.com/)
   - Click "New +" → "Web Service"
   - Connect your repository
   - Select the branch (typically `master` or `main`)

3. **Configure the Service**
   - **Name**: Choose a name for your service (e.g., `sampleapi`)
   - **Region**: Choose your preferred region
   - **Branch**: `master` (or your main branch)
   - **Runtime**: Docker
   - **Plan**: Free (or select a paid plan for better performance)

4. **Add Environment Variables**
   Go to the "Environment" tab and add:
   ```
   ApiSettings__ApiKey=your-secure-api-key-here
   ASPNETCORE_ENVIRONMENT=Production
   ASPNETCORE_URLS=http://+:8080
   ```

5. **Configure Health Check**
   - **Health Check Path**: `/`
   - This endpoint returns API status information

6. **Deploy**
   - Click "Create Web Service"
   - Render will build your Docker image and deploy it

## Post-Deployment

### Accessing Your API

Once deployed, your API will be available at:
```
https://your-service-name.onrender.com
```

### Important Endpoints

- **Health Check**: `https://your-service-name.onrender.com/`
- **Swagger UI**: `https://your-service-name.onrender.com/swagger`
- **API Products**: `https://your-service-name.onrender.com/api/products`

### Testing Your Deployment

1. **Test the health check endpoint**
   ```bash
   curl https://your-service-name.onrender.com/
   ```

2. **Access Swagger UI**
   Open `https://your-service-name.onrender.com/swagger` in your browser

3. **Test API with Authentication**
   ```bash
   curl -H "X-API-Key: your-api-key" \
        https://your-service-name.onrender.com/api/products
   ```

## Important Notes

### Free Tier Limitations

- **Spin Down**: Free tier services spin down after 15 minutes of inactivity
- **Spin Up**: First request after spin down takes ~30 seconds
- **Build Time**: Initial build takes 2-5 minutes
- **Monthly Hours**: 750 hours/month limit

### Security Considerations

1. **API Key**: Always use a strong, randomly generated API key in production
2. **HTTPS**: Render provides free SSL certificates automatically
3. **CORS**: Current configuration allows all origins - consider restricting in production
4. **Environment Variables**: Never commit sensitive data to Git

### Updating Your Deployment

Render automatically redeploys when you push to your connected branch:

```bash
git add .
git commit -m "Update API"
git push origin master
```

### Monitoring

- View logs in the Render dashboard under "Logs" tab
- Set up notifications for deployment failures
- Monitor application health through the health check endpoint

### Custom Domain (Optional)

1. Go to your service settings
2. Click "Add Custom Domain"
3. Follow instructions to configure DNS

## Troubleshooting

### Build Fails

- Check the build logs in Render dashboard
- Ensure Dockerfile is in the repository root
- Verify .NET 8 SDK is being used

### Application Won't Start

- Check environment variables are set correctly
- Review application logs in Render dashboard
- Ensure `ASPNETCORE_URLS` is set to `http://+:8080`

### API Returns 401 Unauthorized

- Verify you're sending the `X-API-Key` header
- Check that `ApiSettings__ApiKey` environment variable matches your header value
- Note: Use double underscores `__` in environment variable names (not colons)

### Slow First Request

- This is normal for free tier after spin down
- Consider upgrading to a paid plan for always-on service
- Or implement a health check monitor to keep service warm

## Support

For Render-specific issues, consult:
- [Render Documentation](https://render.com/docs)
- [Render Community](https://community.render.com/)
- [Render Status](https://status.render.com/)

For API issues, check the application logs and review the API documentation.

